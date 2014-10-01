using System;

namespace CupCake.Host
{
    /// <summary>
    ///     Represents CupCake components
    /// </summary>
    [Flags]
    public enum CupCakeComponents
    {
        /// <summary>
        ///     None.
        /// </summary>
        None = 1,

        /// <summary>
        ///     The CupCake.Messages.dll
        /// </summary>
        Messages = 2 << 0,

        /// <summary>
        ///     The CupCake.Keys.dll
        /// </summary>
        Keys = 2 << 1,

        /// <summary>
        ///     The CupCake.Potions.dll
        /// </summary>
        Potions = 2 << 2,

        /// <summary>
        ///     The CupCake.Players.dll
        /// </summary>
        Players = 2 << 3,

        /// <summary>
        ///     The CupCake.Permissions.dll
        /// </summary>
        Permissions = 2 << 4,

        /// <summary>
        ///     The CupCake.World.dll
        /// </summary>
        World = 2 << 5,

        /// <summary>
        ///     The CupCake.Room.dll
        /// </summary>
        Room = 2 << 6,

        /// <summary>
        ///     The CupCake.Chat.dll
        /// </summary>
        Chat = 2 << 7,

        /// <summary>
        ///     The CupCake.Command.dll
        /// </summary>
        Command = 2 << 8,

        /// <summary>
        ///     The CupCake.Upload.dll
        /// </summary>
        Upload = 2 << 9,

        /// <summary>
        ///     The CupCake.Actions.dll
        /// </summary>
        Actions = 2 << 10,

        /// <summary>
        ///     All CupCake components
        /// </summary>
        All = 2 << 31 - 1
    }
}