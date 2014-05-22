using CupCake.EE.Events.Receive;
using CupCake.Players.Services;

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