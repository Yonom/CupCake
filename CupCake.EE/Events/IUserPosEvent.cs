namespace CupCake.EE.Events
{
    internal interface IUserPosEvent : IUserEvent
    {
        int UserPosX { get; }
        int UserPosY { get; }
    }
}