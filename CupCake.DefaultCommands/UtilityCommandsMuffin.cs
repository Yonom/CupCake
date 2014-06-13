using CupCake.DefaultCommands.Commands.Utility;

namespace CupCake.DefaultCommands
{
    public class UtilityCommandsMuffin : CupCakeMuffin<UtilityCommandsMuffin>
    {
        protected override void Enable()
        {
            this.EnablePart<PingCommand>();
            this.EnablePart<HiCommand>();
            this.EnablePart<HelpCommand>();
            this.EnablePart<SayCommand>();
            this.EnablePart<SayRawCommand>();
            this.EnablePart<AccessCommand>();
            this.EnablePart<AutoSayCommand>();
            this.EnablePart<BlueCommand>();
            this.EnablePart<CompleteLevelCommand>();
            this.EnablePart<DieCommand>(); 
            this.EnablePart<GetCrownCommand>();
            this.EnablePart<GiveWootCommand>();
            this.EnablePart<GreenCommand>();
            this.EnablePart<KillRoomCommand>();
            this.EnablePart<ModModeCommand>(); 
            this.EnablePart<RedCommand>();
            this.EnablePart<SetSmileyCommand>();
            this.EnablePart<UsePotionCommand>();
        }
    }
}