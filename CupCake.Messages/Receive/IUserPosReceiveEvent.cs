namespace CupCake.Messages.Receive
{
    public interface IUserPosReceiveEvent : IUserReceiveEvent
    {
        int UserPosX { get; set; }
        int UserPosY { get; set; }
    }
}