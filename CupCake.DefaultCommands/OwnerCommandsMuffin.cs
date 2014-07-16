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
        public PotionsOffCommand PotionsOffCommand { get; private set; }
        public PotionsOnCommand PotionsOnCommand { get; private set; }
        public ResetCommand ResetCommand { get; private set; }
        public RespawnAllCommand RespawnAllCommand { get; private set; }
        public SaveCommand SaveCommand { get; private set; }
        public SetKeyCommand SetKeyCommand { get; private set; }
        public VisibleCommand VisibleCommand { get; private set; }

        protected override void Enable()
        {
            ClearCommand = EnablePart<ClearCommand>();
            EnablePotsCommand = EnablePart<EnablePotsCommand>();
            KillAllCommand = EnablePart<KillAllCommand>();
            LoadlevelCommand = EnablePart<LoadlevelCommand>();
            NameCommand = EnablePart<NameCommand>();
            PotionsOffCommand = EnablePart<PotionsOffCommand>();
            PotionsOnCommand = EnablePart<PotionsOnCommand>();
            ResetCommand = EnablePart<ResetCommand>();
            RespawnAllCommand = EnablePart<RespawnAllCommand>();
            SaveCommand = EnablePart<SaveCommand>();
            SetKeyCommand = EnablePart<SetKeyCommand>();
            VisibleCommand = EnablePart<VisibleCommand>();
        }
    }
}