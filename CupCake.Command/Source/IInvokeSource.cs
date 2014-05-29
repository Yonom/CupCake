using CupCake.Permissions;

namespace CupCake.Command.Source
{
    public interface IInvokeSource
    {
        object Sender { get; }
        Group Group { get; }
        void Reply(string message);
    }
}