namespace CupCake.Messages.Events.Send
{
    public interface IEncryptedSendEvent
    {
        string Encryption { get; set; }
    }
}