using CupCake.Messages.Receive;

namespace CupCake.Players.Join
{
    public class AddJoinArgs : JoinArgs
    {
        public AddJoinArgs(PlayerService playerService, AddReceiveEvent addReceiveEvent)
            : base(playerService)
        {
            this.AddReceiveEvent = addReceiveEvent;
        }

        public AddReceiveEvent AddReceiveEvent { get; private set; }
    }
}