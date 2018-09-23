﻿using System;
using System.IO;
using Shared.Enums;
using Shared.Handlers;
using Shared.Helpers;
using Sora.Objects;
using Sora.Packets.Client;
using Sora.Server;

namespace Sora.Handler
{
    internal class PacketHandler
    {
        [Handler(HandlerTypes.PacketHandler)]
        public void HandlePackets(Req req, Res res)
        {
            try
            {
                if (!req.Headers.ContainsKey("osu-token")) {
                    Handlers.ExecuteHandler(HandlerTypes.LoginHandler, req, res);
                    return;
                }
                var pr = Presences.GetPresence(req.Headers["osu-token"]);
                if (pr == null)
                {
                    res.StatusCode = 403;
                    return;
                }

                var len = 0;
                while (req.Reader.BaseStream.Length != len)
                {
                    var packetId = (PacketId) req.Reader.ReadInt16();
                    req.Reader.ReadBoolean();
                    var packetData = req.Reader.ReadBytes();
                    len += 2 + 4 + packetData.Length;
                    var packetDataReader = new MStreamReader(new MemoryStream(packetData));
                    switch (packetId)
                    {
                        case PacketId.ClientSendUserStatus:
                            var x = new SendUserStatus();
                            x.ReadFromStream(packetDataReader);
                            Handlers.ExecuteHandler(HandlerTypes.ClientSendUserStatus, pr, x.Status);
                            break;
                        case PacketId.ClientSendIrcMessage:
                            break;
                        case PacketId.ClientExit:
                            break;
                        case PacketId.ClientRequestStatusUpdate:
                            break;
                        case PacketId.ClientPong:
                            Handlers.ExecuteHandler(HandlerTypes.ClientPong, pr);
                            break;
                        case PacketId.ClientStartSpectating:
                            break;
                        case PacketId.ClientStopSpectating:
                            break;
                        case PacketId.ClientSpectateFrames:
                            break;
                        case PacketId.ClientErrorReport:
                            break;
                        case PacketId.ClientCantSpectate:
                            break;
                        case PacketId.ClientSendIrcMessagePrivate:
                            break;
                        case PacketId.ClientLobbyPart:
                            break;
                        case PacketId.ClientLobbyJoin:
                            break;
                        case PacketId.ClientMatchCreate:
                            break;
                        case PacketId.ClientMatchJoin:
                            break;
                        case PacketId.ClientMatchPart:
                            break;
                        case PacketId.ClientMatchChangeSlot:
                            break;
                        case PacketId.ClientMatchReady:
                            break;
                        case PacketId.ClientMatchLock:
                            break;
                        case PacketId.ClientMatchChangeSettings:
                            break;
                        case PacketId.ClientMatchStart:
                            break;
                        case PacketId.ClientMatchScoreUpdate:
                            break;
                        case PacketId.ClientMatchComplete:
                            break;
                        case PacketId.ClientMatchChangeMods:
                            break;
                        case PacketId.ClientMatchLoadComplete:
                            break;
                        case PacketId.ClientMatchNoBeatmap:
                            break;
                        case PacketId.ClientMatchNotReady:
                            break;
                        case PacketId.ClientMatchFailed:
                            break;
                        case PacketId.ClientMatchHasBeatmap:
                            break;
                        case PacketId.ClientMatchSkipRequest:
                            break;
                        case PacketId.ClientChannelJoin:
                            break;
                        case PacketId.ClientBeatmapInfoRequest:
                            break;
                        case PacketId.ClientMatchTransferHost:
                            break;
                        case PacketId.ClientFriendAdd:
                            break;
                        case PacketId.ClientFriendRemove:
                            break;
                        case PacketId.ClientMatchChangeTeam:
                            break;
                        case PacketId.ClientChannelLeave:
                            break;
                        case PacketId.ClientReceiveUpdates:
                            break;
                        case PacketId.ClientSetIrcAwayMessage:
                            break;
                        case PacketId.ClientUserStatsRequest:
                            break;
                        case PacketId.ClientInvite:
                            break;
                        case PacketId.ClientMatchChangePassword:
                            break;
                        case PacketId.ClientSpecialMatchInfoRequest:
                            break;
                        case PacketId.ClientUserPresenceRequest:
                            break;
                        case PacketId.ClientUserPresenceRequestAll:
                            break;
                        case PacketId.ClientUserToggleBlockNonFriendPm:
                            break;
                        case PacketId.ClientMatchAbort:
                            break;
                        case PacketId.ClientSpecialJoinMatchChannel:
                            break;
                        case PacketId.ClientSpecialLeaveMatchChannel:
                            break;
                        default:
                            Program.Logger.Debug("[Unknown Packet] " + packetId);
                            break;
                    }
                }
                if (pr.LastRequest)
                    Presences.EndPresence(pr, true);
                }
            catch
            {
                // ignored
            }
        }
    }
}
