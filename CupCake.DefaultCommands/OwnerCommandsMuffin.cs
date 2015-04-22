using CupCake.DefaultCommands.Commands.Owner;

namespace CupCake.DefaultCommands
{
    public class OwnerCommandsMuffin : CupCakeMuffin<OwnerCommandsMuffin>
    {
        public ClearCommand ClearCommand { get; private set; }
        public EnablePotsCommand EnablePotsCommand { get; private set; }
        public KillAllCommand KillAllCommand { get; private set; }
        public LoadlevelCommand LoadlevelCommand { get; private set; }
        public NameCommand NameCommand { get; private set; }
        public ResetCommand ResetCommand { get; private set; }
        public RespawnAllCommand RespawnAllCommand { get; private set; }
        public SaveCommand SaveCommand { get; private set; }
        public SetKeyCommand SetKeyCommand { get; private set; }
        public VisibleCommand VisibleCommand { get; private set; }

        protected override void Enable()
        {
            this.ClearCommand = this.EnablePart<ClearCommand>();
            this.EnablePotsCommand = this.EnablePart<EnablePotsCommand>();
            this.KillAllCommand = this.EnablePart<KillAllCommand>();
            this.LoadlevelCommand = this.EnablePart<LoadlevelCommand>();
            this.NameCommand = this.EnablePart<NameCommand>();
            this.ResetCommand = this.EnablePart<ResetCommand>();
            this.RespawnAllCommand = this.EnablePart<RespawnAllCommand>();
            this.SaveCommand = this.EnablePart<SaveCommand>();
            this.SetKeyCommand = this.EnablePart<SetKeyCommand>();
            this.VisibleCommand = this.EnablePart<VisibleCommand>();
        }
    }
}