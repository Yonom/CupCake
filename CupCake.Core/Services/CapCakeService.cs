using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
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
