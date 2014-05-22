namespace CupCake.EE.Events
{
    public interface IUserPosEvent : IUserEvent
    {
        int UserPosX { get; }
        int UserPosY { get; }
    }
}