using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CupCake.Permissions;

namespace CupCake.Command.Source
{
    public class InvokeSourceBase : IInvokeSource
    {
        private readonly ReplyCallback _onReply;

        public InvokeSourceBase(object sender, Group @group, ReplyCallback onReply)
        {
            this.PluginName = "Bot";

            this._onReply = onReply;
            this.Sender = sender;
            this.Group = @group;
        }

        public string PluginName { get; set; }
        public object Sender { get; private set; }
        public Group Group { get; private set; }

        public void Reply(string message)
        {
            this._onReply(this.PluginName, message);
        }
    }
}
