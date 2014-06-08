namespace CupCake.Messages.Receive
{
    public interface IMetadataReceiveMessage
    {
        int CurrentWoots { get; set; }
        string OwnerUsername { get; set; }
        int Plays { get; set; }
        int TotalWoots { get; set; }
        string WorldName { get; set; }
    }
}
