using PlayerIOClient;

namespace CupCake.EE.Messages.Receive
{
    public sealed class CoinReceiveMessage : ReceiveMessage
    {
        public readonly int Coins;
        public readonly int UserId;

        internal CoinReceiveMessage(Message message)
            : base(message)
        {
            this.UserId = message.GetInteger(0);
            this.Coins = message.GetInteger(1);
        }
    }
}