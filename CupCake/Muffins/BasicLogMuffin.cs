using System;
using System.Collections.Generic;
using System.Text;
using CupCake.API.Muffins;
using CupCake.EE.Messages.Receive;
using CupCake.Log;

namespace CupCake.Muffins
{
    public class BasicLogMuffin : CupCakeMuffin
    {
        protected override void Enable()
        {
            this.EventsPlatform.Event<SayReceiveMessage>().Bind(OnSay);
        }

        private void OnSay(object sender, SayReceiveMessage e)
        {
            this.Logger.Log(LogPriority.Message, e.Text);
        }
    }
}
