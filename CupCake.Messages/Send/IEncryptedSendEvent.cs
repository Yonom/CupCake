namespace CupCake.Messages.Send
{
    /// <summary>
    ///     Interface Encrypted Send Event
    /// </summary>
    public interface IEncryptedSendEvent
    {
        /// <summary>
        ///     Gets or sets the encryption string.
        /// </summary>
        /// <value>
        ///     The encryption string.
        /// </value>
        string Encryption { get; set; }
    }
}