﻿using System.ComponentModel.Composition;
using MuffinFramework.Muffins;

namespace CupCake.Muffins
{
    public abstract class CupCakeMuffin : CupCakeMuffin<object>
    {
    }

    [InheritedExport(typeof(IMuffin))]
    public abstract class CupCakeMuffin<TProtocol> : CupCakeMuffinPart<TProtocol>, IMuffin
    {
    }
}