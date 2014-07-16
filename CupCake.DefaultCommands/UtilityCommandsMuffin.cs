using CupCake.DefaultCommands.Commands.Utility;

namespace CupCake.DefaultCommands
{
    public sealed class UtilityCommandsMuffin : CupCakeMuffin<UtilityCommandsMuffin>
    {
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

        protected override void Enable()
        {
            PingCommand = EnablePart<PingCommand>();
            HiCommand = EnablePart<HiCommand>();
            HelpCommand = EnablePart<HelpCommand>();
            SayCommand = EnablePart<SayCommand>();
            SayRawCommand = EnablePart<SayRawCommand>();
            AccessCommand = EnablePart<AccessCommand>();
            AutoSayCommand = EnablePart<AutoSayCommand>();
            BlueCommand = EnablePart<BlueCommand>();
            CompleteLevelCommand = EnablePart<CompleteLevelCommand>();
            DieCommand = EnablePart<DieCommand>();
            GetCrownCommand = EnablePart<GetCrownCommand>();
            GiveWootCommand = EnablePart<GiveWootCommand>();
            GreenCommand = EnablePart<GreenCommand>();
            KillRoomCommand = EnablePart<KillRoomCommand>();
            ModModeCommand = EnablePart<ModModeCommand>();
            RedCommand = EnablePart<RedCommand>();
            SetSmileyCommand = EnablePart<SetSmileyCommand>();
            UsePotionCommand = EnablePart<UsePotionCommand>();
        }
    }
}