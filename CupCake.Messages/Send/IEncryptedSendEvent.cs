namespace CupCake.Messages.Send
{
    public interface IEncryptedSendEvent
    {
        string Encryption { get; set; }
    }
}