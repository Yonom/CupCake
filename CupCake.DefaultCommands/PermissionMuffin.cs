using System;
using CupCake.Command;
using CupCake.Command.Source;
using CupCake.Core.Events;
using CupCake.Core.Log;
using CupCake.Core.Storage;
using CupCake.DefaultCommands.Commands.Permissions;
using CupCake.Messages.User;
using CupCake.Permissions;
using CupCake.Players;

namespace CupCake.DefaultCommands
{
    public class PermissionMuffin : CupCakeMuffin<PermissionMuffin>
    {
        private const string PermissionsId = "CCPerm";
        private const Group MinGodGroup = Group.Moderator;

        protected override void Enable()
        {
            this.Events.Bind<JoinPlayerEvent>(this.OnJoin, EventPriority.High);
            this.Events.Bind<ChangedPermissionEvent>(this.OnChangedPermission);

            this.EnablePart<BanMuffinPart>();

            this.EnablePart<GetRankCommand>();
            this.EnablePart<AdminCommand>();
            this.EnablePart<OpCommand>();
            this.EnablePart<ModCommand>();
            this.EnablePart<TrustCommand>();
            this.EnablePart<UserCommand>();
            this.EnablePart<LimitCommand>();
        }

        private void OnChangedPermission(object sender, ChangedPermissionEvent e)
        {
            // Give or remove edit if permissions change
            if (this.RoomService.AccessRight >= AccessRight.Owner)
            {
                if (e.OldPermission < MinGodGroup && e.NewPermission >= MinGodGroup)
                    this.Chatter.GiveEdit(e.Player.Username);
                else if (e.OldPermission >= MinGodGroup && e.NewPermission < MinGodGroup)
                    this.Chatter.RemoveEdit(e.Player.Username);
            }

            // Save permissions, if its not directly loaded from db
            if (e.Player.GetRankLoaded())
            {
                if (e.NewPermission != Group.Moderator && e.NewPermission != Group.Host)
                {
                    this.SetPermission(e.Player.StorageName, e.NewPermission);
                }
                else if (e.OldPermission >= Group.Moderator)
                {
                    // Downgrade the player to User
                    this.SetPermission(e.Player.StorageName, Group.User);
                }
            }
        }

        public void SetPermission(string username, Group group)
        {
            try
            {
                this.StoragePlatform.Set(PermissionsId, username, group.ToString());
            }
            catch (StorageException ex)
            {
                this.Logger.Log(LogPriority.Error,
                    "Unable to save permissions for user " + username + ". " + ex.Message);
            }
        }

        private void OnJoin(object sender, JoinPlayerEvent e)
        {
            try
            {
                var groupStr = this.StoragePlatform.Get(PermissionsId, e.Player.StorageName);
                Group group;
                Enum.TryParse(groupStr, true, out group);

                switch (group)
                {
                    case Group.Banned:
                        this.PermissionService.Ban(e.Player);
                        break;

                    case Group.Limited:
                        this.PermissionService.Limit(e.Player);
                        break;

                    case Group.Trusted:
                        this.PermissionService.Trust(e.Player);
                        break;

                    case Group.Operator:
                        this.PermissionService.Op(e.Player);
                        break;

                    case Group.Admin:
                        this.PermissionService.Admin(e.Player);
                        break;
                }
            }
            catch (StorageException ex)
            {
                this.Logger.Log(LogPriority.Error, "Unable to load permissions for user " + e.Player.Username + ". " + ex.Message);
            }

            e.Player.SetRankLoaded(true);
        }
    }
}
