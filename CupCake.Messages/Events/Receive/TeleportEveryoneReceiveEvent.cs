using System.Collections.Generic;
using PlayerIOClient;

namespace CupCake.Messages.Events.Receive
{
    public class TeleportEveryoneReceiveEvent : ReceiveEvent
    {
        public TeleportEveryoneReceiveEvent(Message message)
            : base(message)
        {
            var coords = new Dictionary<int, Point>();
            this.Coordinates = new ReadOnlyDictionary<int, Point>(coords);

            this.ResetCoins = message.GetBoolean(0);

            for (uint i = 1; i <= message.Count - 1u; i += 3)
            {
                coords.Add(message.GetInteger(i),
                    new Point(message.GetInteger(i + 1u), message.GetInteger(i + 2u)));
            }
        }

        public ReadOnlyDictionary<int, Point> Coordinates { get; private set; }
        public bool ResetCoins { get; private set; }
    }
}