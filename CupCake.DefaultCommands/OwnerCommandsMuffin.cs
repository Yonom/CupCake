using System.Runtime.CompilerServices;
using CupCake.DefaultCommands.Commands.Owner;

namespace CupCake.DefaultCommands
{
    public class OwnerCommandsMuffin : CupCakeMuffin<OwnerCommandsMuffin>
    {
        protected override void Enable()
        {
            this.EnablePart<ClearCommand>();
            this.EnablePart<EnablePotsCommand>();
            this.EnablePart<KillAllCommand>();
            this.EnablePart<LoadlevelCommand>();
            this.EnablePart<NameCommand>();
            this.EnablePart<PotionsOffCommand>();
            this.EnablePart<PotionsOnCommand>();
            this.EnablePart<ResetCommand>(); 
            this.EnablePart<RespawnAllCommand>();
            this.EnablePart<SaveCommand>();
            this.EnablePart<SetKeyCommand>();
            this.EnablePart<VisibleCommand>();
        }
    }
}