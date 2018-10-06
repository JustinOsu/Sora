﻿#region copyright
/*
MIT License

Copyright (c) 2018 Robin A. P.

Permission is hereby granted, free of charge, to any person obtaining a copy
of this software and associated documentation files (the "Software"), to deal
in the Software without restriction, including without limitation the rights
to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
copies of the Software, and to permit persons to whom the Software is
furnished to do so, subject to the following conditions:

The above copyright notice and this permission notice shall be included in all
copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
SOFTWARE.
*/
#endregion

using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using Amib.Threading;
using JetBrains.Annotations;
using Shared.Enums;
using Shared.Handlers;
using Shared.Helpers;

namespace Sora.Server
{
    public class Req
    {
        public Dictionary<string, string> Headers = new Dictionary<string, string>();
        public MStreamReader Reader;
        public HttpMethods Method;
        public string Path;
        public string Ip;
    }

    public class Res
    {
        public Dictionary<string, string> Headers = new Dictionary<string, string>();
        public int StatusCode { get; set; } = 200;
        public MStreamWriter Writer;
    }

    public class Client
    {
        private readonly TcpClient _client;
        public Client(TcpClient client) => _client = client;

        public void DoStuff()
        {
            using (_client)
            {
                try
                {
                    var sr = new StreamReader(_client.GetStream());
                    var req = ReadHeader(sr);
                    var res = new Res {Writer = MStreamWriter.New()};
                    if (req == null)
                    {
                        req = new Req
                        {
                            Headers = {["Host"] = "Unknown"},
                            Method = HttpMethods.Get
                        };
                    }
                    if (req.Method == HttpMethods.Get)
                    {
                        res.Writer.WriteRawString(
                            !File.Exists("index.html")
                                ? @"<html><head><title>Sora - an Strategic bancho.</title></head><body></body></html>"
                                : File.ReadAllText("index.html"));
                        WriteRes(_client, req, res);
                        return;
                    }
                    Handlers.ExecuteHandler(HandlerTypes.PacketHandler, req, res);
                    WriteRes(_client, req, res);
                }
                catch (Exception ex)
                {
                    Logger.L.Error(ex);
                }
            }
        }

        public Req ReadHeader(StreamReader rd)
        {
            var x = new Req();
            var mainHeader = rd.ReadLine();
            if (mainHeader == null) return x;
            var hSplit = mainHeader.Split(' ');
            if (hSplit.Length < 3) return null;
            Enum.TryParse(hSplit[0], true, out HttpMethods method);
            x.Method = method;
            x.Path = hSplit[1];

            while (true)
            {
                var currentLine = rd.ReadLine();
                if (currentLine == null) break;
                if (currentLine.Trim() == "") break;
                var headSplit = currentLine.Split(':', 2);
                if (headSplit.Length < 2) break;
                x.Headers[headSplit[0].Trim()] = headSplit[1].Trim();
            }

            if (x.Method != HttpMethods.Post) return x;

            if (!x.Headers.ContainsKey("Content-Length"))
                return null;

            int.TryParse(x.Headers["Content-Length"], out var byteLength);

            var data = new byte[byteLength];
            for (var i = 0; i < byteLength; i++)
                data[i] = (byte) rd.Read();

            x.Reader = new MStreamReader(new MemoryStream(data));
            x.Ip = _client.Client.RemoteEndPoint.ToString().Split(':')[0];
            return x;
        }

        public static void WriteRes(TcpClient c, Req r, Res s)
        {
            using (var stream = c.GetStream())
            {
                var header = Encoding.ASCII.GetBytes("HTTP/1.1 "+s.StatusCode+" " + (HttpStatusCode)s.StatusCode + "\r\n" + Header2Str(r, s) + "\r\n");
                stream.Write(header);
                stream.Write(s.Writer.ToArray());
                s.Writer.Close();
            }
        }

        public static string Header2Str(Req x, Res s)
        {
            var outputStr = string.Empty;
            s.Headers["cho-protocol"] = "19";
            s.Headers  ["Connection"] = "keep-alive";
            s.Headers  ["Keep-Alive"] = "timeout=5, max=100";
            s.Headers["Content-Type"] = "text/html; charset=UTF-8";
            s.Headers        ["Host"] = x.Headers["Host"];
            s.Headers  ["cho-server"] = "Sora (https://github.com/Mempler/Sora)";
            foreach (var key in s.Headers.Keys)
                outputStr += $"{key}: {s.Headers[key]}\r\n";
            return outputStr;
        }
    }
    public class HttpServer
    {
        private bool _running;
        private readonly TcpListener _listener;
        private readonly SmartThreadPool _pool;
        private Thread _serverThread;

        public HttpServer(short port)
        {
            _running = false;
            _listener = new TcpListener(IPAddress.Any, port);
            _pool = new SmartThreadPool();
        }

        public Thread Run()
        {
            if (_running)
                throw new Exception("Address already in use!");
            var x = new Thread(_RunServer);
            x.Start();
            _serverThread = x;
            return x;
        }
        
        [UsedImplicitly]
        public void Stop()
        {
            if (_running)
                throw new Exception("Cannot stop while already stopped!");
            
            Thread.Sleep(100);
            _serverThread.Abort();
        }

        private void _RunServer()
        {
            _listener.Start();
            _pool.Start();
            _running = true;
            _pool.MaxThreads = 8 * Environment.ProcessorCount; _pool.MinThreads = _pool.MaxThreads;
            while (true)
            {
                if (!_running)
                {
                    _listener.Stop();
                    break;
                }

                _pool.QueueWorkItem(new Client(_listener.AcceptTcpClient()).DoStuff);
            }
        }
    }

    public enum HttpMethods
    {
        Get,
        Post,
    }
}
