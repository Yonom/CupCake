using BotBits;
using BotBits.Events;

namespace CupCake.Permissions
{
    public sealed class PermissionService : Service
    {
        [EventListener(EventPriority.High)]
        private void OnJoin(JoinEvent e)
        {
            e.Player.MetadataChanged +=
                (o, args) =>
                {
                    if (args.Key == "Group")
                        this.Events.Raise(new ChangedPermissionEvent(e.Player, (Group)args.OldValue,
                            (Group)args.NewValue));
                };
        }

        public void Admin(Player player)
        {
            player.SetGroup(Group.Admin);
        }

        public void Op(Player player)
        {
            player.SetGroup(Group.Operator);
        }

        public void Mod(Player player)
        {
            player.SetGroup(Group.Moderator);
        }

        public void Trust(Player player)
        {
            player.SetGroup(Group.Trusted);
        }

        public void User(Player player)
        {
            player.SetGroup(Group.User);
        }

        public void Limit(Player player)
        {
            player.SetGroup(Group.Limited);
        }

        public void Ban(Player player)
        {
            player.SetGroup(Group.Banned);
        }
    }
}