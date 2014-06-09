using System;
using System.Globalization;
using CupCake.Core.Log;
using CupCake.Core.Storage;
using CupCake.DefaultCommands.Commands.Ban;
using CupCake.Messages.User;
using CupCake.Permissions;
using CupCake.Players;

namespace CupCake.DefaultCommands
{
    public class BanMuffin : CupCakeMuffin<BanMuffin>
    {
        private const string BanReasonsId = "CCBamR";
        private const string BanTimeoutsId = "CCBanT";

        protected override void Enable()
        {
            this.Events.Bind<ChangedPermissionEvent>(this.OnChangedPermission);

            this.EnablePart<BanCommand>();
            this.EnablePart<TempBanCommand>();
        }

        private void OnChangedPermission(object sender, ChangedPermissionEvent e)
        {
            if (e.NewPermission == Group.Banned && RoomService.AccessRight == AccessRight.Owner)
            {

                string reason = "Banned!";
                if (this.GetBanTimeout(e.Player) != default(DateTime))
                {
                    reason += " Until: " + this.GetBanTimeout(e.Player).ToString("g");
                    
                    // Check if ban has not expired already
                    if (this.GetBanTimeout(e.Player) <= DateTime.UtcNow)
                    {
                        this.SetBanReason(e.Player, null);
                        this.SetBanTimeout(e.Player, default(DateTime));
                        PermissionService.User(e.Player);
                        return;
                    }
                }
                if (this.GetBanReason(e.Player) != null)
                {
                    reason += " Reason: " + this.GetBanReason(e.Player);
                }
                Chatter.Kick(e.Player.Username, reason);
            }
        }

        public void Ban(Player player)
        {
            PermissionService.Ban(player);
        }

        public void Ban(Player player, string reason)
        {
            SetBanReason(player, reason);
            PermissionService.Ban(player);
        }

        public void Ban(Player player, DateTime timeout)
        {
            SetBanTimeout(player, timeout);
            PermissionService.Ban(player);
        }

        public void Ban(Player player, DateTime timeout, string reason)
        {
            SetBanTimeout(player, timeout);
            SetBanReason(player, reason);
            PermissionService.Ban(player);
        }

        private string GetBanReason(Player p)
        {
            if (p.HasBanReason())
                return p.GetBanReason();

            try
            {
                return this.StoragePlatform.Get(BanReasonsId, p.StorageName);
            }
            catch (StorageException ex)
            {
                this.Logger.Log(LogPriority.Error, "Unable to load ban reason for user " + p.Username + ". " + ex.Message);
            }

            return null;
        }

        private void SetBanReason(Player p, string reason)
        {
            p.SetBanReason(reason);

            try
            {
                this.StoragePlatform.Set(BanReasonsId, p.StorageName, reason);
            }
            catch (StorageException ex)
            {
                this.Logger.Log(LogPriority.Error, "Unable to save ban reason for user " + p.Username + ". " + ex.Message);
            }
        }

        private DateTime GetBanTimeout(Player p)
        {
            if (p.HasBanTimeout())
                return p.GetBanTimeout();

            try
            {
                var timeoutStr = this.StoragePlatform.Get(BanTimeoutsId, p.StorageName);
                if (timeoutStr != null)
                {
                    var timeout = DateTime.Parse(timeoutStr);
                    p.SetBanTimeout(timeout);
                    return timeout;
                }
            }
            catch (StorageException ex)
            {
                this.Logger.Log(LogPriority.Error, "Unable to load ban timeout for user " + p.Username + ". " + ex.Message);
            }

            return default(DateTime);
        }

        private void SetBanTimeout(Player p, DateTime timeout)
        {
            p.SetBanTimeout(timeout);

            try
            {
                this.StoragePlatform.Set(BanTimeoutsId, p.StorageName, timeout.ToString(CultureInfo.InvariantCulture));
            }
            catch (StorageException ex)
            {
                this.Logger.Log(LogPriority.Error, "Unable to save ban timeout for user " + p.Username + ". " + ex.Message);
            }
        }

    }
}
