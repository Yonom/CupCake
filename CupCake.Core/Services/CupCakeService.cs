using System.ComponentModel.Composition;
using MuffinFramework.Services;

namespace CupCake.Core.Services
{
    public abstract class CupCakeService : CupCakeService<object>
    {
    }

    [InheritedExport(typeof(IService))]
    public abstract class CupCakeService<TProtocol> : CupCakeServicePart<TProtocol>, IService
    {
    }
}