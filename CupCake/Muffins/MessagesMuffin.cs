using CupCake.API.Muffins;
using CupCake.EE.Events.Receive;
using CupCake.EE.Events.Send;
using CupCake.Log;
using CupCake.Messages;
using PlayerIOClient;

namespace CupCake.Muffins
{
    public class MessagesMuffin : CupCakeMuffin
    {
        public MessageManager MessageManager { get; private set; }

        protected override void Enable()
        {
            // Init MessageManager
            this.MessageManager = new MessageManager(this.EventsPlatform);

            // Register all EE messages
            this.MessageManager.RegisterMessage<InitReceiveEvent>("init");
            this.MessageManager.RegisterMessage<InfoReceiveEvent>("info");
            this.MessageManager.RegisterMessage<UpgradeReceiveEvent>("upgrade");
            this.MessageManager.RegisterMessage<UpdateMetaReceiveEvent>("updatemeta");
            this.MessageManager.RegisterMessage<ShowKeyReceiveEvent>("show");
            this.MessageManager.RegisterMessage<HideKeyReceiveEvent>("hide");
            this.MessageManager.RegisterMessage<AddReceiveEvent>("add");
            this.MessageManager.RegisterMessage<LeftReceiveEvent>("left");
            this.MessageManager.RegisterMessage<MoveReceiveEvent>("m");
            this.MessageManager.RegisterMessage<FaceReceiveEvent>("face");
            this.MessageManager.RegisterMessage<PotionReceiveEvent>("p");
            this.MessageManager.RegisterMessage<CoinReceiveEvent>("c");
            this.MessageManager.RegisterMessage<CrownReceiveEvent>("k");
            this.MessageManager.RegisterMessage<SilverCrownReceiveEvent>("ks");
            this.MessageManager.RegisterMessage<MagicReceiveEvent>("w");
            this.MessageManager.RegisterMessage<LevelUpReceiveEvent>("levelup");
            this.MessageManager.RegisterMessage<GodModeReceiveEvent>("god");
            this.MessageManager.RegisterMessage<ModModeReceiveEvent>("mod");
            this.MessageManager.RegisterMessage<WootUpReceiveEvent>("wu");
            this.MessageManager.RegisterMessage<AllowPotionsReceiveEvent>("allowpotions");
            this.MessageManager.RegisterMessage<KillReceiveEvent>("kill");
            this.MessageManager.RegisterMessage<AccessReceiveEvent>("access");
            this.MessageManager.RegisterMessage<LostAccessReceiveEvent>("lostaccess");
            this.MessageManager.RegisterMessage<ResetReceiveEvent>("reset");
            this.MessageManager.RegisterMessage<TeleportEveryoneReceiveEvent>("tele");
            this.MessageManager.RegisterMessage<TeleportPlayerReceiveEvent>("teleport");
            this.MessageManager.RegisterMessage<SaveDoneReceiveEvent>("saved");
            this.MessageManager.RegisterMessage<ClearReceiveEvent>("clear");
            this.MessageManager.RegisterMessage<SayReceiveEvent>("say");
            this.MessageManager.RegisterMessage<SayOldReceiveEvent>("say_old");
            this.MessageManager.RegisterMessage<AutoTextReceiveEvent>("autotext");
            this.MessageManager.RegisterMessage<WriteReceiveEvent>("write");
            this.MessageManager.RegisterMessage<BlockPlaceReceiveEvent>("b");
            this.MessageManager.RegisterMessage<CoinDoorPlaceReceiveEvent>("bc");
            this.MessageManager.RegisterMessage<SoundPlaceReceiveEvent>("bs");
            this.MessageManager.RegisterMessage<RotatablePlaceReceiveEvent>("br");
            this.MessageManager.RegisterMessage<PortalPlaceReceiveEvent>("pt");
            this.MessageManager.RegisterMessage<WorldPortalPlaceReceiveEvent>("wp");
            this.MessageManager.RegisterMessage<LabelPlaceReceiveEvent>("lb");
            this.MessageManager.RegisterMessage<LabelPlaceReceiveEvent>("ts");
            this.MessageManager.RegisterMessage<GiveWizardReceiveEvent>("givewizard");
            this.MessageManager.RegisterMessage<GiveFireWizardReceiveEvent>("givewizard2");
            this.MessageManager.RegisterMessage<GiveDarkWizardReceiveEvent>("givedarkwizard");
            this.MessageManager.RegisterMessage<GiveWitchReceiveEvent>("givewitch");
            this.MessageManager.RegisterMessage<GiveGrinchReceiveEvent>("givegrinch");
            this.MessageManager.RegisterMessage<RefreshShopReceiveEvent>("refreshshop");

            // Bind OnMessage
            this.ConnectionPlatform.Connection.OnMessage += this.Connection_OnMessage;

            // Bind SendEvent event
            this.EventsPlatform.Event<SendEvent>().Bind(this.OnSendEvent);

            // Bind all SendEvent events
            this.EventsPlatform.Event<InitSendEvent>().Bind(this.OnAnySendEvent);
            this.EventsPlatform.Event<Init2SendEvent>().Bind(this.OnAnySendEvent);
            this.EventsPlatform.Event<BlockPlaceSendEvent>().Bind(this.OnAnySendEvent);
            this.EventsPlatform.Event<CoinDoorPlaceSendEvent>().Bind(this.OnAnySendEvent);
            this.EventsPlatform.Event<RotatablePlaceSendEvent>().Bind(this.OnAnySendEvent);
            this.EventsPlatform.Event<SoundPlaceSendEvent>().Bind(this.OnAnySendEvent);
            this.EventsPlatform.Event<PortalPlaceSendEvent>().Bind(this.OnAnySendEvent);
            this.EventsPlatform.Event<WorldPortalPlaceSendEvent>().Bind(this.OnAnySendEvent);
            this.EventsPlatform.Event<LabelPlaceSendEvent>().Bind(this.OnAnySendEvent);
            this.EventsPlatform.Event<CoinSendEvent>().Bind(this.OnAnySendEvent);
            this.EventsPlatform.Event<PressRedKeySendEvent>().Bind(this.OnAnySendEvent);
            this.EventsPlatform.Event<PressBlueKeySendEvent>().Bind(this.OnAnySendEvent);
            this.EventsPlatform.Event<PressGreenKeySendEvent>().Bind(this.OnAnySendEvent);
            this.EventsPlatform.Event<GetCrownSendEvent>().Bind(this.OnAnySendEvent);
            this.EventsPlatform.Event<TouchDiamondSendEvent>().Bind(this.OnAnySendEvent);
            this.EventsPlatform.Event<TouchCakeSendEvent>().Bind(this.OnAnySendEvent);
            this.EventsPlatform.Event<CompleteLevelSendEvent>().Bind(this.OnAnySendEvent);
            this.EventsPlatform.Event<GodModeSendEvent>().Bind(this.OnAnySendEvent);
            this.EventsPlatform.Event<ModModeSendEvent>().Bind(this.OnAnySendEvent);
            this.EventsPlatform.Event<MoveSendEvent>().Bind(this.OnAnySendEvent);
            this.EventsPlatform.Event<SaySendEvent>().Bind(this.OnAnySendEvent);
            this.EventsPlatform.Event<AutoSaySendEvent>().Bind(this.OnAnySendEvent);
            this.EventsPlatform.Event<AccessSendEvent>().Bind(this.OnAnySendEvent);
            this.EventsPlatform.Event<ChangeFaceSendEvent>().Bind(this.OnAnySendEvent);
            this.EventsPlatform.Event<SaveWorldSendEvent>().Bind(this.OnAnySendEvent);
            this.EventsPlatform.Event<ChangeWorldNameSendEvent>().Bind(this.OnAnySendEvent);
            this.EventsPlatform.Event<ChangeWorldEditKeySendEvent>().Bind(this.OnAnySendEvent);
            this.EventsPlatform.Event<ClearWorldSendEvent>().Bind(this.OnAnySendEvent);
            this.EventsPlatform.Event<KillWorldSendEvent>().Bind(this.OnAnySendEvent);
            this.EventsPlatform.Event<PotionSendEvent>().Bind(this.OnAnySendEvent);
            this.EventsPlatform.Event<AllowPotionsSendEvent>().Bind(this.OnAnySendEvent);
            this.EventsPlatform.Event<WootUpSendEvent>().Bind(this.OnAnySendEvent);
            this.EventsPlatform.Event<DeathSendEvent>().Bind(this.OnAnySendEvent);
            this.EventsPlatform.Event<CheckpointSendEvent>().Bind(this.OnAnySendEvent);
            this.EventsPlatform.Event<TouchPlayerSendEvent>().Bind(this.OnAnySendEvent);
        }

        private void Connection_OnMessage(object sender, Message e)
        {
            if (this.MessageManager.Contains(e.Type))
            {
                this.MessageManager[e.Type].Invoke(sender, e);
            }
            else
            {
                this.Logger.Log(LogPriority.Debug, "Received unregistered message with type: " + e.Type);
            }
        }

        private void OnAnySendEvent(object sender, SendEvent e)
        {
            this.EventsPlatform.Event<SendEvent>().Raise(sender, e);
        }

        private void OnSendEvent(object sender, SendEvent e)
        {
            if (!e.Cancelled)
            {
                this.ConnectionPlatform.Connection.Send(e.GetMessage());
            }
        }
    }
}