using CupCake.EE.Events.Receive;
using CupCake.Players.Services;

namespace CupCake.Players.Join
{
    public class InitJoinArgs : JoinArgs
    {
        public InitJoinArgs(PlayerService playerService, InitReceiveEvent initReceiveEvent)
            : base(playerService)
        {
            this.InitReceiveEvent = initReceiveEvent;
        }

        public InitReceiveEvent InitReceiveEvent { get; private set; }
    }
}