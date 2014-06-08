using CupCake.HostAPI.Title;
using CupCake.Messages.Receive;

namespace CupCake.Server.Muffins
{
    public class TitleMuffin : CupCakeMuffin
    {
        protected override void Enable()
        {
            this.Events.Bind<InitReceiveEvent>(this.OnInit);
            this.Events.Bind<UpdateMetaReceiveEvent>(this.OnUpdateMeta);
        }
        
        private void OnUpdateMeta(object sender, UpdateMetaReceiveEvent e)
        {
            this.Events.Raise(new ChangeTitleEvent(e.WorldName));
        }

        private void OnInit(object sender, InitReceiveEvent e)
        {
            this.Events.Raise(new ChangeTitleEvent(e.WorldName));
        }
    }
}
