using CupCake.Messages.Receive;

namespace CupCake.Players.Join
{
    public class AddJoinArgs : JoinArgs
    {
        public AddJoinArgs(PlayerService playerService, AddReceiveEvent addReceiveEvent)
            : base(playerService)
        {
            AddReceiveEvent = addReceiveEvent;
        }

        public AddReceiveEvent AddReceiveEvent { get; private set; }
    }
}