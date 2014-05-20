using System;
using CupCake.API.Muffins;
using CupCake.EE.Events.Receive;
using CupCake.EE.Events.Send;

namespace CupCake.Muffins
{
    public class EEMuffin : CupCakeMuffin
    {
        protected override void Enable()
        {
            this.MuffinLoader.EnableComplete += this.MuffinLoader_EnableComplete;

            this.EventsPlatform.Event<InitReceiveEvent>().Bind(this.OnInit);
        }

        private void MuffinLoader_EnableComplete(object sender, EventArgs e)
        {
            this.EventsPlatform.Event<InitSendEvent>().Raise(this, new InitSendEvent());
        }

        private void OnInit(object sender, InitReceiveEvent e)
        {
            this.EventsPlatform.Event<Init2SendEvent>().Raise(this, new Init2SendEvent());
        }
    }
}