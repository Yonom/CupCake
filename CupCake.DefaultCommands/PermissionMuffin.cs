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
    public sealed class PermissionMuffin : CupCakeMuffin<PermissionMuffin>
    {
        private const string PermissionsId = "CCPerm";
        private const Group MinGodGroup = Group.Moderator;

        public BanMuffinPart BanMuffinPart { get; private set; }

        public GetRankCommand GetRankCommand { get; private set; }
        public AdminCommand AdminCommand { get; private set; }
        public OpCommand OpCommand { get; private set; }
        public ModCommand ModCommand { get; private set; }
        public TrustCommand TrustCommand { get; private set; }
        public UserCommand UserCommand { get; private set; }
        public LimitCommand LimitCommand { get; private set; }

        protected override void Enable()
        {
            Events.Bind<JoinPlayerEvent>(OnJoin, EventPriority.High);
            Events.Bind<ChangedPermissionEvent>(OnChangedPermission, EventPriority.Lowest);

            BanMuffinPart = EnablePart<BanMuffinPart>();

            GetRankCommand = EnablePart<GetRankCommand>();
            AdminCommand = EnablePart<AdminCommand>();
            OpCommand = EnablePart<OpCommand>();
            ModCommand = EnablePart<ModCommand>();
            TrustCommand = EnablePart<TrustCommand>();
            UserCommand = EnablePart<UserCommand>();
            LimitCommand = EnablePart<LimitCommand>();
        }

        private void OnChangedPermission(object sender, ChangedPermissionEvent e)
        {
            // Give or remove edit if permissions change
            if (RoomService.AccessRight >= AccessRight.Owner && e.Player.Username != RoomService.Owner)
            {
                if (e.OldPermission < MinGodGroup && e.NewPermission >= MinGodGroup)
                    Chatter.GiveEdit(e.Player.Username);
                else if (e.OldPermission >= MinGodGroup && e.NewPermission < MinGodGroup)
                    Chatter.RemoveEdit(e.Player.Username);
            }

            // Save permissions, if its not directly loaded from db
            if (e.Player.GetRankLoaded())
            {
                if (e.NewPermission != Group.Moderator && e.NewPermission != Group.Host)
                {
                    SetPermission(e.Player.StorageName, e.NewPermission);
                }
                else if (e.OldPermission >= Group.Moderator)
                {
                    // Downgrade the player to User
                    SetPermission(e.Player.StorageName, Group.User);
                }
            }
        }

        public void SetPermission(string storageName, Group group)
        {
            try
            {
                StoragePlatform.Set(PermissionsId, storageName, group.ToString());
            }
            catch (StorageException ex)
            {
                Logger.Log(LogPriority.Error,
                    "Unable to save permissions for user " + storageName + ". " + ex.Message);
            }
        }

        public Group GetPermission(string storageName)
        {
            string groupStr = StoragePlatform.Get(PermissionsId, storageName);
            Group group;
            Enum.TryParse(groupStr, true, out group);
            return group;
        }

        private void OnJoin(object sender, JoinPlayerEvent e)
        {
            try
            {
                e.Player.SetGroup(GetPermission(e.Player.StorageName));
            }
            catch (StorageException ex)
            {
                Logger.Log(LogPriority.Error,
                    "Unable to load permissions for user " + e.Player.Username + ". " + ex.Message);
            }

            e.Player.SetRankLoaded(true);
        }
    }
}