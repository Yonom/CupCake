using System;
using CupCake.Messages.Receive;
using CupCake.Messages.Send;

namespace CupCake
{
    internal class EEMuffin : CupCakeMuffin
    {
        protected override void Enable()
        {
            MuffinLoader.EnableComplete += MuffinLoader_EnableComplete;

            Events.Bind<InitReceiveEvent>(OnInit);
        }

        private void MuffinLoader_EnableComplete(object sender, EventArgs e)
        {
            Events.Raise(new InitSendEvent());
        }

        private void OnInit(object sender, InitReceiveEvent e)
        {
            Events.Raise(new Init2SendEvent());
        }
    }
}