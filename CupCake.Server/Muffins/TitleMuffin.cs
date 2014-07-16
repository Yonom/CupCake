using CupCake.HostAPI.Title;
using CupCake.Messages.Receive;

namespace CupCake.Server.Muffins
{
    public class TitleMuffin : CupCakeMuffin
    {
        protected override void Enable()
        {
            Events.Bind<InitReceiveEvent>(OnInit);
            Events.Bind<UpdateMetaReceiveEvent>(OnUpdateMeta);
        }

        private void OnUpdateMeta(object sender, UpdateMetaReceiveEvent e)
        {
            Events.Raise(new ChangeTitleEvent(e.WorldName));
        }

        private void OnInit(object sender, InitReceiveEvent e)
        {
            Events.Raise(new ChangeTitleEvent(e.WorldName));
        }
    }
}