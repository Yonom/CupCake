using System;
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
            if (this.RoomService.AccessRight >= AccessRight.Owner && e.Player.Username != this.RoomService.Owner)
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

        public void SetPermission(string storageName, Group group)
        {
            try
            {
                this.StoragePlatform.Set(PermissionsId, storageName, group.ToString());
            }
            catch (StorageException ex)
            {
                this.Logger.Log(LogPriority.Error,
                    "Unable to save permissions for user " + storageName + ". " + ex.Message);
            }
        }

        public Group GetPermission(string storageName)
        {
            string groupStr = this.StoragePlatform.Get(PermissionsId, storageName);
            Group group;
            Enum.TryParse(groupStr, true, out group);
            return group;
        }

        private void OnJoin(object sender, JoinPlayerEvent e)
        {
            try
            {
                e.Player.SetGroup(this.GetPermission(e.Player.StorageName));
            }
            catch (StorageException ex)
            {
                this.Logger.Log(LogPriority.Error,
                    "Unable to load permissions for user " + e.Player.Username + ". " + ex.Message);
            }

            e.Player.SetRankLoaded(true);
        }
    }
}