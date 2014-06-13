using CupCake.Permissions;

namespace CupCake.Command.Source
{
    public delegate void ReplyCallback(string pluginName, string message);

    public interface IInvokeSource
    {
        string PluginName { get; set; }
        object Sender { get; }
        Group Group { get; }
        string Name { get; }
        void Reply(string message);
    }
}