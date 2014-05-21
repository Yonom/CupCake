using System.ComponentModel.Composition;
using MuffinFramework.Services;

namespace CupCake.Core.Services
{
    [InheritedExport(typeof(IService))]
    public abstract class CupCakeService<TProtocol> : CupCakeServicePart<TProtocol>, IService
    {
    }
}