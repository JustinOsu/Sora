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
using System.Linq;
using JetBrains.Annotations;
using MaxMind.GeoIP2.Responses;
using Shared.Database.Models;
using Shared.Enums;
using Shared.Handlers;
using Shared.Helpers;
using Shared.Server;
using Sora.Enums;
using Sora.Helpers;
using Sora.Objects;
using Sora.Packets.Server;

namespace Sora.Handler
{
    internal class LoginHandler
    {
        [UsedImplicitly]
        [Handler(HandlerTypes.LoginHandler)]
        public void OnLogin(Req req, Res res)
        {
            Presence pr = new Presence();
            res.Headers["cho-token"] = pr.Token;
            try
            {
                Login loginData = LoginParser.ParseLogin(req.Reader);
                if (loginData == null)
                {
                    Exception(res);
                    return;
                }

                Users user = Users.GetUser(Users.GetUserId(loginData.Username));
                if (user == null)
                {
                    LoginFailed(res);
                    return;
                }

                if (!user.IsPassword(loginData.Password))
                {
                    LoginFailed(res);
                    return;
                }

                pr.User = user;

                if (req.Ip != "127.0.0.1" && req.Ip != "0.0.0.0")
                {
                    CityResponse data = Localisation.GetData(req.Ip);
                    pr.CountryId = Localisation.StringToCountryId(data.Country.IsoCode);
                    if (data.Location.Longitude != null) pr.Lon = (double) data.Location.Longitude;
                    if (data.Location.Latitude != null) pr.Lat  = (double) data.Location.Latitude;
                }

                pr.LeaderboardStd   = LeaderboardStd.GetLeaderboard(pr.User);
                pr.LeaderboardRx    = LeaderboardRx.GetLeaderboard(pr.User);
                pr.LeaderboardTouch = LeaderboardTouch.GetLeaderboard(pr.User);

                pr.Timezone         = loginData.Timezone;
                pr.BlockNonFriendDm = loginData.BlockNonFriendDMs;

                LPresences.BeginPresence(pr);

                Success(res, user.Id);
                res.Writer.Write(new ProtocolNegotiation());
                res.Writer.Write(new UserPresence(pr));
                res.Writer.Write(new PresenceBundle(LPresences.GetUserIds(pr).ToList()));
                res.Writer.Write(new HandleUpdate(pr));

                foreach (Channel chanAuto in Channels.ChannelsAutoJoin)
                {
                    if (chanAuto.AdminOnly && pr.User.HasPrivileges(Privileges.Admin))
                        res.Writer.Write(new ChannelAvailableAutojoin(chanAuto));
                    else if (!chanAuto.AdminOnly)
                        res.Writer.Write(new ChannelAvailableAutojoin(chanAuto));

                    if (chanAuto.JoinChannel(pr))
                        res.Writer.Write(new ChannelJoinSuccess(chanAuto));
                    else
                        res.Writer.Write(new ChannelRevoked(chanAuto));
                }

                foreach (KeyValuePair<string, Channel> chan in Channels.Channels_)
                    if (chan.Value.AdminOnly && pr.User.HasPrivileges(Privileges.Admin))
                        res.Writer.Write(new ChannelAvailable(chan.Value));
                    else if (!chan.Value.AdminOnly)
                        res.Writer.Write(new ChannelAvailable(chan.Value));

                PacketStream stream = LPacketStreams.GetStream("main");
                if (stream == null)
                {
                    Exception(res);
                    return;
                }

                stream.Join(pr);
            }
            catch (Exception ex) { Logger.L.Error(ex); }
        }

        private static void LoginFailed(Res res) { res.Writer.Write(new LoginResponse(LoginResponses.Failed)); }

        private static void Exception(Res res) { res.Writer.Write(new LoginResponse(LoginResponses.Exception)); }

        private static void Success(Res res, int userid)
        {
            res.Writer.Write(new LoginResponse((LoginResponses) userid));
        }
    }
}