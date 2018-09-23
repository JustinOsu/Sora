﻿using System;
using System.Collections.Generic;
using System.IO;
using Shared.Database.Models;
using Shared.Enums;
using Shared.Helpers;
using Shared.Interfaces;
using Sora.Packets.Client;
using Sora.Packets.Server;

namespace Sora.Objects
{
    public static class Presences
    {
        private static readonly Dictionary<string, Presence> presences = new Dictionary<string, Presence>();
        public static int UserCount;

        public static Presence GetPresence(string token) => presences.TryGetValue(token, out var pr) ? pr : null;

        public static List<int> GetUserIds()
        {
            var l = new List<int>();
            foreach (var presence in presences)
                l.Add(presence.Value.User.Id);
            return l;
        }
        public static List<int> GetUserIds(Presence pr)
        {
            var l = new List<int>();
            foreach (var presence in presences)
            {
                if (presence.Value.User.Id == pr.User.Id) continue;
                l.Add(presence.Value.User.Id);
            }
            return l;
        }
        
        public static void BeginPresence(Presence presence)
        {
            presence.BeginSeason = DateTime.Now;
            presences[presence.Token] = presence;
            UserCount++;
        }
        public static void EndPresence(Presence presence, bool forceful)
        {
            if (forceful && presences.ContainsKey(presence.Token))
            {
                presences[presence.Token] = null;
                return;
            }

            presence.LastRequest = true;
            presence.Stream.Write(new Announce("Your session has ended!"));
            UserCount--;
        }
    }
    public class Presence
    {
        public string Token;
        public MStreamWriter Stream;
        public DateTime BeginSeason;
        public bool LastRequest;

        public Users User;
        public UserStats Stats;
        public LeaderboardStd LeaderboardStd;
        public LeaderboardRx LeaderboardRx;
        public LeaderboardTouch LeaderboardTouch;
        public UserStatus Status = new UserStatus { BeatmapChecksum = "", StatusText = "" }; // Predefined strings to prevent Issues.

        public uint Rank;

        public bool BlockNonFriendDm;
        public int ClientPermissions;

        public CountryIds CountryId;
        public double Lon;
        public double Lat;
        public byte Timezone;

        public bool Touch;
        public bool Relax;
        public bool Disconnected;

        public Presence()
        {
            Token = Guid.NewGuid().ToString();
            var str = new MemoryStream();
            Stream = new MStreamWriter(str);
        }

        protected bool Equals(Presence pr)
        {
            return Token == pr.Token;
        }

        public void Write(IPacketSerializer p) => Stream.Write(p);
    }
}
