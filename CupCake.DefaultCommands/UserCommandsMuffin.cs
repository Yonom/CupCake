using CupCake.DefaultCommands.Commands.User;

namespace CupCake.DefaultCommands
{
    public sealed class UserCommandsMuffin : CupCakeMuffin<UserCommandsMuffin>
    {
        public KickCommand KickCommand { get; private set; }
        public KillCommand KillCommand { get; private set; }
        public TeleportCommand TeleportCommand { get; private set; }
        public GiveEditCommand GiveEditCommand { get; private set; }
        public RemoveEditCommand RemoveEditCommand { get; private set; }
        public MuteCommand MuteCommand { get; private set; }
        public UnmuteCommand UnmuteCommand { get; private set; }
        public ReportAbuseCommand ReportAbuseCommand { get; private set; }

        protected override void Enable()
        {
            this.KickCommand = this.EnablePart<KickCommand>();
            this.KillCommand = this.EnablePart<KillCommand>();
            this.TeleportCommand = this.EnablePart<TeleportCommand>();
            this.GiveEditCommand = this.EnablePart<GiveEditCommand>();
            this.RemoveEditCommand = this.EnablePart<RemoveEditCommand>();
            this.MuteCommand = this.EnablePart<MuteCommand>();
            this.UnmuteCommand = this.EnablePart<UnmuteCommand>();
            this.ReportAbuseCommand = this.EnablePart<ReportAbuseCommand>();
        }
    }
}