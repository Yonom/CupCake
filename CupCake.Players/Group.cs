namespace CupCake.Players
{
    public enum Group
    {
        /// <summary>
        ///     Banned. May not join any world and therefore shouldn't be given access to any commands.
        /// </summary>
        Banned = -200,

        /// <summary>
        ///     The pre-ban stage. If there are any commands available to normal users, this group will not have access to them.
        /// </summary>
        Limited = -100,

        /// <summary>
        ///     Default rank
        /// </summary>
        User = 0,

        /// <summary>
        ///     These users can kick normal users and might be able to test alpha stage commands. Don't give them permament
        ///     advantage over normal users!
        /// </summary>
        Trusted = 100,

        /// <summary>
        ///     This rank is temprorary, most level bot commands (other than setting wins for example) should use this rank.
        /// </summary>
        Moderator = 200,

        /// <summary>
        ///     Should have the same rights as admins, excluding setting ranks.
        /// </summary>
        Operator = 300,

        /// <summary>
        ///     Admins have access to absolutely everything except console-only commands. Most commands should use the operator
        ///     group instead of this.
        /// </summary>
        Admin = 400,

        /// <summary>
        ///     This rank is used to specify console-only commands; no real users will have it.
        /// </summary>
        Host = 500,
    }
}