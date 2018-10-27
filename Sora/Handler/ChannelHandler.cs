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

using Shared.Enums;
using Shared.Handlers;
using Sora.Objects;
using Sora.Packets.Server;

namespace Sora.Handler
{
    internal class ChannelHandler
    {
        [Handler(HandlerTypes.ClientChannelJoin)]
        public void OnChannelJoin(Presence pr, string channelName)
        {
            Channel channel;
            switch (channelName)
            {
                case "#spectator":
                    channel = pr.Spectator?.SpecChannel;
                    break;
                case "#multiplayer":
                    channel = pr.JoinedRoom?.Channel;
                    break;
                default:
                    channel = LChannels.GetChannel(channelName);
                    break;
            }

            if (channel == null)
            {
                pr.Write(new ChannelRevoked(channelName));
                return;
            }

            channel.LeaveChannel(pr); // leave channel before joining to fix some Issues.

            if (channel.JoinChannel(pr))
                pr.Write(new ChannelJoinSuccess(channel));

            channel.BoundStream?.Broadcast(new ChannelAvailable(channel));
        }

        [Handler(HandlerTypes.ClientChannelLeave)]
        public void OnChannelLeave(Presence pr, string channelName)
        {
            Channel channel;
            switch (channelName)
            {
                case "#spectator":
                    channel = pr.Spectator?.SpecChannel;
                    break;
                case "#multiplayer":
                    channel = pr.JoinedRoom?.Channel;
                    break;
                default:
                    channel = LChannels.GetChannel(channelName);
                    break;
            }

            if (channel == null)
            {
                pr.Write(new ChannelRevoked(channelName));
                return;
            }

            channel.LeaveChannel(pr);

            channel.BoundStream?.Broadcast(new ChannelAvailable(channel));
        }
    }
}
