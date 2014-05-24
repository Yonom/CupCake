namespace CupCake.EE.Events.Send
{
    public interface IEncryptedSendEvent
    {
        string Encryption { get; set; }
    }
}