using CupCake.API.Muffins;
using CupCake.EE.Messages.Receive;
using CupCake.Log;

namespace CupCake.Muffins
{
    public class BasicLogMuffin : CupCakeMuffin
    {
        protected override void Enable()
        {
            this.EventsPlatform.Event<SayReceiveMessage>().Bind(this.OnSay);
        }

        private void OnSay(object sender, SayReceiveMessage e)
        {
            this.Logger.Log(LogPriority.Message, e.Text);
        }
    }
}