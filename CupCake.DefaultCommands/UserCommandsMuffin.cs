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
            KickCommand = EnablePart<KickCommand>();
            KillCommand = EnablePart<KillCommand>();
            TeleportCommand = EnablePart<TeleportCommand>();
            GiveEditCommand = EnablePart<GiveEditCommand>();
            RemoveEditCommand = EnablePart<RemoveEditCommand>();
            MuteCommand = EnablePart<MuteCommand>();
            UnmuteCommand = EnablePart<UnmuteCommand>();
            ReportAbuseCommand = EnablePart<ReportAbuseCommand>();
        }
    }
}