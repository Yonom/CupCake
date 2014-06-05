using System;
using CupCake.Messages.Receive;
using CupCake.Messages.Send;

namespace CupCake
{
    public class EEMuffin : CupCakeMuffin
    {
        protected override void Enable()
        {
            this.MuffinLoader.EnableComplete += this.MuffinLoader_EnableComplete;

            this.Events.Bind<InitReceiveEvent>(this.OnInit);
        }

        private void MuffinLoader_EnableComplete(object sender, EventArgs e)
        {
            this.Events.Raise(new InitSendEvent());
        }

        private void OnInit(object sender, InitReceiveEvent e)
        {
            this.Events.Raise(new Init2SendEvent());
        }
    }
}