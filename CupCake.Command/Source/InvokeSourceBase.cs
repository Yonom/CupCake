using CupCake.Permissions;

namespace CupCake.Command.Source
{
    public class InvokeSourceBase : IInvokeSource
    {
        private readonly ReplyCallback _onReply;

        public InvokeSourceBase(object sender, Group @group, string name, ReplyCallback onReply)
        {
            PluginName = "Bot";

            _onReply = onReply;
            Sender = sender;
            Group = @group;
            Name = name;
        }

        public string PluginName { get; set; }
        public object Sender { get; private set; }
        public Group Group { get; private set; }
        public string Name { get; private set; }

        public void Reply(string message)
        {
            _onReply(PluginName, message);
        }
    }
}