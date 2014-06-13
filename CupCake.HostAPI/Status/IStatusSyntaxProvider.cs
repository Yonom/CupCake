namespace CupCake.HostAPI.Status
{
    public interface IStatusSyntaxProvider
    {
        string Parse(StatusItem[] statuses);
    }
}