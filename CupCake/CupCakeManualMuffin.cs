using MuffinFramework.Muffins;

namespace CupCake
{
    /// <summary>
    ///     Class CupCakeManualMuffin.
    /// </summary>
    public abstract class CupCakeManualMuffin : CupCakeManualMuffin<object>
    {
    }

    /// <summary>
    ///     Class CupCakeManualMuffin.
    /// </summary>
    /// <typeparam name="TProtocol">The type of the protocol.</typeparam>
    public abstract class CupCakeManualMuffin<TProtocol> : CupCakeMuffinPart<TProtocol>, IMuffin
    {
    }
}