using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using MuffinFramework.Muffins;

namespace CupCake.API.Muffins
{
    public abstract class CupCakeMuffin : CupCakeMuffin<object>
    {
    }

    [InheritedExport(typeof(IMuffin))]
    public abstract class CupCakeMuffin<TProtocol> : CupCakeMuffinPart<TProtocol>, IMuffin
    {
    }
}
