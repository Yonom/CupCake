using CupCake.DefaultCommands.Commands.Utility;

namespace CupCake.DefaultCommands
{
    public sealed class UtilityCommandsMuffin : CupCakeMuffin<UtilityCommandsMuffin>
    {
        public CyanCommand CyanCommand { get; private set; }
        public MagentaCommand MagentaCommand { get; private set; }
        public YellowCommand YellowCommand { get; private set; }
        public PingCommand PingCommand { get; private set; }
        public HiCommand HiCommand { get; private set; }
        public HelpCommand HelpCommand { get; private set; }
        public SayCommand SayCommand { get; private set; }
        public SayRawCommand SayRawCommand { get; private set; }
        public AccessCommand AccessCommand { get; private set; }
        public AutoSayCommand AutoSayCommand { get; private set; }
        public BlueCommand BlueCommand { get; private set; }
        public CompleteLevelCommand CompleteLevelCommand { get; private set; }
        public DieCommand DieCommand { get; private set; }
        public GetCrownCommand GetCrownCommand { get; private set; }
        public GiveWootCommand GiveWootCommand { get; private set; }
        public GreenCommand GreenCommand { get; private set; }
        public KillRoomCommand KillRoomCommand { get; private set; }
        public ModModeCommand ModModeCommand { get; private set; }
        public RedCommand RedCommand { get; private set; }
        public SetSmileyCommand SetSmileyCommand { get; private set; }
        public UsePotionCommand UsePotionCommand { get; private set; }
        public ShutdownCommand ShutdownCommand { get; private set; }
        public RestartCommand RestartCommand { get; private set; }

        protected override void Enable()
        {     
            this.CyanCommand = this.EnablePart<CyanCommand>();
            this.MagentaCommand = this.EnablePart<MagentaCommand>();
            this.YellowCommand = this.EnablePart<YellowCommand>();
            this.PingCommand = this.EnablePart<PingCommand>();
            this.HiCommand = this.EnablePart<HiCommand>();
            this.HelpCommand = this.EnablePart<HelpCommand>();
            this.SayCommand = this.EnablePart<SayCommand>();
            this.SayRawCommand = this.EnablePart<SayRawCommand>();
            this.AccessCommand = this.EnablePart<AccessCommand>();
            this.AutoSayCommand = this.EnablePart<AutoSayCommand>();
            this.BlueCommand = this.EnablePart<BlueCommand>();
            this.CompleteLevelCommand = this.EnablePart<CompleteLevelCommand>();
            this.DieCommand = this.EnablePart<DieCommand>();
            this.GetCrownCommand = this.EnablePart<GetCrownCommand>();
            this.GiveWootCommand = this.EnablePart<GiveWootCommand>();
            this.GreenCommand = this.EnablePart<GreenCommand>();
            this.KillRoomCommand = this.EnablePart<KillRoomCommand>();
            this.ModModeCommand = this.EnablePart<ModModeCommand>();
            this.RedCommand = this.EnablePart<RedCommand>();
            this.SetSmileyCommand = this.EnablePart<SetSmileyCommand>();
            this.UsePotionCommand = this.EnablePart<UsePotionCommand>();
            this.ShutdownCommand = this.EnablePart<ShutdownCommand>();
            this.RestartCommand = this.EnablePart<RestartCommand>();
        }
    }
}