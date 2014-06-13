using System;
using System.Globalization;
using CupCake.Command;
using CupCake.Core.Log;
using CupCake.Core.Storage;
using CupCake.DefaultCommands.Commands.Ban;
using CupCake.Messages.User;
using CupCake.Permissions;
using CupCake.Players;

namespace CupCake.DefaultCommands
{
    public class BanMuffinPart : CupCakeMuffinPart<PermissionMuffin>
    {
        private const string BanReasonsId = "CCBanR";
        private const string BanTimeoutsId = "CCBanT";

        protected override void Enable()
        {
            this.Events.Bind<ChangedPermissionEvent>(this.OnChangedPermission);

            this.EnablePart<BanCommand, BanMuffinPart>();
            this.EnablePart<TempBanCommand, BanMuffinPart>();
        }

        public void SetPermission(string username, Group group)
        {
            Host.SetPermission(username, group);
        }

        private void OnChangedPermission(object sender, ChangedPermissionEvent e)
        {
            if (e.NewPermission == Group.Banned)
            {
                if (RoomService.AccessRight == AccessRight.Owner)
                {
                    var kicktext = "Banned!";
                    var timeout = this.GetBanTimeout(e.Player.StorageName);
                    var reason = this.GetBanReason(e.Player.StorageName);

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

                // Save ban parameters
                if (e.Player.GetRankLoaded())
                {
                    this.SetBanReason(e.Player.StorageName, e.Player.GetBanReason());
                    this.SetBanTimeout(e.Player.StorageName, e.Player.GetBanTimeout());
                }

                // Get ban parameters
                if (!e.Player.GetRankLoaded())
                {
                    e.Player.SetBanReason(this.GetBanReason(e.Player.StorageName));
                    e.Player.SetBanTimeout(this.GetBanTimeout(e.Player.StorageName));
                }
            }
        }

        public string GetBanReason(string name)
        {
            try
            {
                return this.StoragePlatform.Get(BanReasonsId, name);
            }
            catch (StorageException ex)
            {
                this.Logger.Log(LogPriority.Error, "Unable to load ban reason for user " + name + ". " + ex.Message);
            }

            return null;
        }

        public void SetBanReason(string name, string reason)
        {
            try
            {
                this.StoragePlatform.Set(BanReasonsId, name, reason);
            }
            catch (StorageException ex)
            {
                this.Logger.Log(LogPriority.Error, "Unable to save ban reason for user " + name + ". " + ex.Message);
            }
        }

        public DateTime GetBanTimeout(string name)
        {
            try
            {
                var timeoutStr = this.StoragePlatform.Get(BanTimeoutsId, name);
                if (timeoutStr != null)
                {
                    var timeout = DateTime.Parse(timeoutStr);
                    return timeout;
                }
            }
            catch (StorageException ex)
            {
                this.Logger.Log(LogPriority.Error, "Unable to load ban timeout for user " + name + ". " + ex.Message);
            }

            return default(DateTime);
        }

        public void SetBanTimeout(string name, DateTime timeout)
        {
            try
            {
                this.StoragePlatform.Set(BanTimeoutsId, name, timeout.ToString(CultureInfo.InvariantCulture));
            }
            catch (StorageException ex)
            {
                this.Logger.Log(LogPriority.Error, "Unable to save ban timeout for user " + name + ". " + ex.Message);
            }
        }
    }
}
