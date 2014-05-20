using CupCake.API.Muffins;
using CupCake.EE.Events.Receive;
using CupCake.Log;

namespace CupCake.Muffins
{
    public class BasicLogMuffin : CupCakeMuffin
    {
        protected override void Enable()
        {
            this.Events.Bind<SayReceiveEvent>(this.OnSay);
        }

        private void OnSay(object sender, SayReceiveEvent e)
        {
            this.Logger.Log(LogPriority.Message, e.Text);
        }
    }
}