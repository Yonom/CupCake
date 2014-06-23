using System.ComponentModel.Composition;
using MuffinFramework.Muffins;

namespace CupCake
{
    /// <summary>
    /// Class CupCakeMuffin.
    /// </summary>
    public abstract class CupCakeMuffin : CupCakeMuffin<object>
    {
    }

    /// <summary>
    /// Class CupCakeMuffin.
    /// </summary>
    /// <typeparam name="TProtocol">The type of the protocol.</typeparam>
    [InheritedExport(typeof(IMuffin))]
    public abstract class CupCakeMuffin<TProtocol> : CupCakeMuffinPart<TProtocol>, IMuffin
    {
    }
}