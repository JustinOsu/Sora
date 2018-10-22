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
using System.IO;
using Newtonsoft.Json;

namespace Shared.Helpers
{
    public class Config
    {
        public MySql MySql = new MySql
            {Database = "gigamons", Hostname = "127.0.0.1", Username = "root", Port = 3306, Password = ""};
        public Server Server = new Server {Hostname = "localhost", Port = 5001};
        
        public static Config ReadConfig()
        {
            if (File.Exists("config.json")) return JsonConvert.DeserializeObject<Config>(File.ReadAllText("config.json"));
            Config cfg = new Config();

            File.WriteAllText("config.json", JsonConvert.SerializeObject(cfg, Formatting.Indented));
            Logger.L.Info("Config has been created! please edit.");
            Environment.Exit(0);

            return cfg;
        }
    }

    public struct Server
    {
        public string Hostname;
        public short Port;
    }

    public struct MySql
    {
        public string Username;
        public string Password;
        public string Hostname;
        public short Port;
        public string Database;
    }
}