using CupCake.Messages.Receive;

namespace CupCake.Players.Join
{
    public class InitJoinArgs : JoinArgs
    {
        public InitJoinArgs(PlayerService playerService, InitReceiveEvent initReceiveEvent)
            : base(playerService)
        {
            InitReceiveEvent = initReceiveEvent;
        }

        public InitReceiveEvent InitReceiveEvent { get; private set; }
    }
}