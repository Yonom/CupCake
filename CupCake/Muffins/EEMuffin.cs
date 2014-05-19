using CupCake.API.Muffins;
using CupCake.EE.Messages.Receive;
using CupCake.EE.Messages.Send;

namespace CupCake.Muffins
{
    public class EEMuffin : CupCakeMuffin
    {
        protected override void Enable()
        {
            this.MuffinLoader.EnableComplete += this.MuffinLoader_EnableComplete;

            this.EventsPlatform.Event<InitReceiveMessage>().Bind(this.OnInit);
        }

        void MuffinLoader_EnableComplete(object sender, System.EventArgs e)
        {
            this.EventsPlatform.Event<InitSendMessage>().Raise(this, new InitSendMessage());
        }

        private void OnInit(object sender, InitReceiveMessage e)
        {
            this.EventsPlatform.Event<Init2SendMessage>().Raise(this, new Init2SendMessage());
        }
    }
}