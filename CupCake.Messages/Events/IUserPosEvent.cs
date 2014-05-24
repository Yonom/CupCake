namespace CupCake.Messages.Events
{
    public interface IUserPosEvent : IUserEvent
    {
        int UserPosX { get; }
        int UserPosY { get; }
    }
}