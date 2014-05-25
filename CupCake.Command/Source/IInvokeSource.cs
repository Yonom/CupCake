using CupCake.Permissions;

namespace CupCake.Command.Source
{
    public interface IInvokeSource
    {
        object Sender { get; }
        Group Group { get; }
        bool Handled { get; set; }
        void Reply(string message);
    }
}