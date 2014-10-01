using MuffinFramework.Services;

namespace CupCake.Core
{
    public abstract class CupCakeManualService : CupCakeManualService<object>
    {
    }

    public abstract class CupCakeManualService<TProtocol> : CupCakeServicePart<TProtocol>, IService
    {
    }
}