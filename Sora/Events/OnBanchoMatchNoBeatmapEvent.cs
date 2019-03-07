using EventManager.Attributes;
using EventManager.Enums;
using Sora.Enums;
using Sora.EventArgs;
using Sora.Objects;

namespace Sora.Events
{
    [EventClass]
    public class OnBanchoMatchNoBeatmapEvent
    {
        [Event(EventType.BanchoMatchNoBeatmap)]
        public void OnBanchoMatchNoBeatmap(BanchoMatchNoBeatmapArgs args)
        {
            if (args.pr.JoinedRoom == null)
                return;
            MultiplayerSlot slot = args.pr.JoinedRoom.GetSlotByUserId(args.pr.User.Id);

            slot.Status = MultiSlotStatus.NoMap;

            args.pr.JoinedRoom.Update();
        }
    }
}