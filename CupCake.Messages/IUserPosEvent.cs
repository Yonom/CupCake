namespace CupCake.Messages
{
    public interface IUserPosEvent : IUserEvent
    {
        int UserPosX { get; }
        int UserPosY { get; }
    }
}