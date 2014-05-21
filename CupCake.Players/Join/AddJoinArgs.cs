using CupCake.EE.Events.Receive;

namespace CupCake.Players.Join
{
    internal class AddJoinArgs : JoinArgs
    {
        public AddReceiveEvent AddReceiveEvent { get; private set; }

        public AddJoinArgs(AddReceiveEvent addReceiveEvent)
        {
            this.AddReceiveEvent = addReceiveEvent;
        }
    }
}
