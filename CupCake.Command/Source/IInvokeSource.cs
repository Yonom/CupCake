namespace CupCake.Command.Source
{
    public interface IInvokeSource
    {
        bool Handled { get; set; }
        void Reply(string message);
    }
}
