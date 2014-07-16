using System;
using System.Globalization;
using CupCake.Core.Events;
using CupCake.Core.Log;
using CupCake.Core.Storage;
using CupCake.DefaultCommands.Commands.Ban;
using CupCake.Messages.User;
using CupCake.Permissions;

namespace CupCake.DefaultCommands
{
    public sealed class BanMuffinPart : CupCakeMuffinPart<PermissionMuffin>
    {
        private const string BanReasonsId = "CCBanR";
        private const string BanTimeoutsId = "CCBanT";

        public BanCommand BanCommand { get; private set; }
        public TempBanCommand TempBanCommand { get; private set; }

        protected override void Enable()
        {
            Events.Bind<ChangedPermissionEvent>(OnChangedPermission, EventPriority.Lowest);

            BanCommand = EnablePart<BanCommand, BanMuffinPart>();
            TempBanCommand = EnablePart<TempBanCommand, BanMuffinPart>();
        }

        internal void SetPermission(string username, Group group)
        {
            Host.SetPermission(username, group);
        }

        internal Group GetPermission(string name)
        {
            return Host.GetPermission(name);
        }

        private void OnChangedPermission(object sender, ChangedPermissionEvent e)
        {
            if (e.NewPermission == Group.Banned)
            {
                // Save ban parameters
                if (e.Player.GetRankLoaded())
                {
                    SetBanReason(e.Player.StorageName, e.Player.GetBanReason());
                    SetBanTimeout(e.Player.StorageName, e.Player.GetBanTimeout());
                }

                // Get ban parameters
                if (!e.Player.GetRankLoaded())
                {
                    e.Player.SetBanReason(GetBanReason(e.Player.StorageName));
                    e.Player.SetBanTimeout(GetBanTimeout(e.Player.StorageName));
                }


                if (RoomService.AccessRight == AccessRight.Owner)
                {
                    string kicktext = "Banned!";
                    DateTime timeout = e.Player.GetBanTimeout();
                    string reason = e.Player.GetBanReason();

                    if (timeout != default(DateTime))
                    {
                        kicktext += String.Format(" Until: {0:g} UTC", timeout);

                        // Check if ban has not expired already
                        if (timeout <= DateTime.UtcNow)
                        {
                            PermissionService.User(e.Player);
                            return;
                        }
                    }
                    if (reason != null)
                    {
                        kicktext += " Reason: " + reason;
                    }
                    Chatter.Kick(e.Player.Username, kicktext);
                }
            }
        }

        public string GetBanReason(string name)
        {
            try
            {
                return StoragePlatform.Get(BanReasonsId, name);
            }
            catch (StorageException ex)
            {
                Logger.Log(LogPriority.Error, "Unable to load ban reason for user " + name + ". " + ex.Message);
            }

            return null;
        }

        public void SetBanReason(string name, string reason)
        {
            try
            {
                StoragePlatform.Set(BanReasonsId, name, reason);
            }
            catch (StorageException ex)
            {
                Logger.Log(LogPriority.Error, "Unable to save ban reason for user " + name + ". " + ex.Message);
            }
        }

        public DateTime GetBanTimeout(string name)
        {
            try
            {
                string timeoutStr = StoragePlatform.Get(BanTimeoutsId, name);
                if (timeoutStr != null)
                {
                    DateTime timeout = DateTime.Parse(timeoutStr, CultureInfo.InvariantCulture);
                    return timeout;
                }
            }
            catch (StorageException ex)
            {
                Logger.Log(LogPriority.Error, "Unable to load ban timeout for user " + name + ". " + ex.Message);
            }

            return default(DateTime);
        }

        public void SetBanTimeout(string name, DateTime timeout)
        {
            try
            {
                StoragePlatform.Set(BanTimeoutsId, name, timeout.ToString(CultureInfo.InvariantCulture));
            }
            catch (StorageException ex)
            {
                Logger.Log(LogPriority.Error, "Unable to save ban timeout for user " + name + ". " + ex.Message);
            }
        }
    }
}