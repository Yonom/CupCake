using System;
using CupCake.Command;
using CupCake.Command.Source;
using CupCake.Core;
using CupCake.Messages.Receive;
using CupCake.Messages.Send;

namespace CupCake
{
    internal class EEMuffin : CupCakeMuffin
    {
        protected override void Enable()
        {
            this.MuffinLoader.EnableComplete += this.MuffinLoader_EnableComplete;
        }

        private void MuffinLoader_EnableComplete(object sender, EventArgs e)
        {
            this.Events.Raise(new InitSendEvent());
        }

        [EventListener]
        private void OnInit(InitReceiveEvent e)
        {
            this.Events.Raise(new Init2SendEvent());
        }

        [Command("HAI")]
        private void OnHai(IInvokeSource source, ParsedCommand message)
        {
            
        }
    }
}