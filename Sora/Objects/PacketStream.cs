﻿#region LICENSE
/*
    Sora - A Modular Bancho written in C#
    Copyright (C) 2019 Robin A. P.

    This program is free software: you can redistribute it and/or modify
    it under the terms of the GNU Affero General Public License as
    published by the Free Software Foundation, either version 3 of the
    License, or (at your option) any later version.

    This program is distributed in the hope that it will be useful,
    but WITHOUT ANY WARRANTY; without even the implied warranty of
    MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
    GNU Affero General Public License for more details.

    You should have received a copy of the GNU Affero General Public License
    along with this program.  If not, see <https://www.gnu.org/licenses/>.
*/
#endregion

using System.Collections.Generic;
using System.Linq;
using Shared.Helpers;
using Shared.Interfaces;

namespace Sora.Objects
{
    public class PacketStream
    {
        private readonly Dictionary<string, Presence> _joinedPresences = new Dictionary<string, Presence>();

        public PacketStream(string name)
        {
            StreamName = name;
        }

        public string StreamName { get; }

        public int JoinedUsers => _joinedPresences.Count;

        public void Join(Presence pr)
        {
            _joinedPresences.Add(pr.Token, pr);
        }

        public void Left(Presence pr)
        {
            _joinedPresences.Remove(pr.Token);
        }

        public void Left(string token)
        {
            _joinedPresences.Remove(token);
        }

        public void Broadcast(IPacket packet, params Presence[] ignorePresences)
        {
            foreach (KeyValuePair<string, Presence> presence in _joinedPresences)
            {
                if (presence.Value == null)
                {
                    Left(presence.Key);
                    continue;
                }

                if (ignorePresences.Contains(presence.Value))
                    continue;

                if (packet == null)
                    Logger.Err("PACKET IS NULL!");

                presence.Value?.Write(packet);
            }
        }
    }
}