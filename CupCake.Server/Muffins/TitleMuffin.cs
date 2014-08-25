using CupCake.Core;
using CupCake.HostAPI.Title;
using CupCake.Messages.Receive;

namespace CupCake.Server.Muffins
{
    public class TitleMuffin : CupCakeMuffin
    {
        protected override void Enable()
        {
        }

        [EventListener]
        private void OnUpdateMeta(UpdateMetaReceiveEvent e)
        {
            this.Events.Raise(new ChangeTitleEvent(e.WorldName));
        }

        [EventListener]
        private void OnInit(InitReceiveEvent e)
        {
            this.Events.Raise(new ChangeTitleEvent(e.WorldName));
        }
    }
}