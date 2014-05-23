using CupCake.Core.Events;
using CupCake.EE.Events.Send;

namespace CupCake.Upload
{
    public class UploadRequestEvent : Event
    {
        public UploadRequestEvent(BlockPlaceSendEvent sendEvent)
        {
            this.SendEvent = sendEvent;
        }

        public BlockPlaceSendEvent SendEvent { get; private set; }

        /// <summary>
        ///     Determines whether the message should be put in the front or back of the queue.
        ///     Must be set before raising the Event.
        /// </summary>
        public bool IsUrgent { get; set; }
    }
}