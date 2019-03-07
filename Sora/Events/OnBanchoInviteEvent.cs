#region LICENSE
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

using EventManager.Attributes;
using EventManager.Enums;
using Sora.EventArgs;
using Sora.Objects;
using Sora.Services;

namespace Sora.Events
{
    [EventClass]
    public class OnBanchoInviteEvent
    {
        private readonly PresenceService _ps;

        public OnBanchoInviteEvent(PresenceService ps)
        {
            _ps = ps;
        }
        
        [Event(EventType.BanchoInvite)]
        public void OnBanchoInvite(BanchoInviteArgs args)
        {
            if (args.pr.JoinedRoom == null) return;
            Presence opr = _ps.GetPresence(args.UserId);
            if (opr == null) return;
            args.pr.JoinedRoom.Invite(args.pr, opr);
        }
    }
}