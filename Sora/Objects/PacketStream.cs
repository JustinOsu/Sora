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

using System.Collections.Generic;
using Shared.Enums;
using Shared.Handlers;
using Shared.Interfaces;

namespace Sora.Objects
{
    public class LPacketStreams
    {
        private static readonly Dictionary<string, PacketStream> PacketStreams = new Dictionary<string, PacketStream>();

        [Handler(HandlerTypes.Initializer)]
        public static void Initialize()
        {
            JoinStream("main", new PacketStream());
        }

        public static PacketStream GetStream(string name)
        {
            PacketStreams.TryGetValue(name, out var x);
            return x;
        }
        public static void JoinStream(string name, PacketStream str) =>
            PacketStreams[name] = str;

        public static void LeaveStream(string name) =>
            PacketStreams[name] = null;
    }

    public class PacketStream
    {
        public Dictionary<string, Presence> JoinedPresences = new Dictionary<string, Presence>();

        public void Join(Presence pr) => JoinedPresences[pr.Token] = pr;

        public void Left(Presence pr) => JoinedPresences[pr.Token] = null;
        public void Left(string token) => JoinedPresences[token] = null;

        public void Broadcast(IPacketSerializer packet, Presence ignorePresence = null)
        {
            foreach (var presence in JoinedPresences)
            {
                if (presence.Value == ignorePresence || presence.Value.Disconnected) continue;
                presence.Value.Write(packet);
            }
        }
    }
}
