using CupCake.API.Muffins;
using CupCake.Core.Services;
using CupCake.EE.Messages;
using CupCake.EE.Messages.Receive;
using CupCake.EE.Messages.Send;
using CupCake.Log.Log;
using PlayerIOClient;

namespace CupCake.EE.Muffins
{
    public class MessagesMuffin : CupCakeMuffin
    {
        public MessageManager MessageManager { get; private set; }

        protected override void Enable()
        {
            // Init MessageManager
            this.MessageManager = new MessageManager(this.EventsPlatform);

            // Register all EE messages
            this.MessageManager.RegisterMessage<InitReceiveMessage>("init");
            this.MessageManager.RegisterMessage<InfoReceiveMessage>("info");
            this.MessageManager.RegisterMessage<UpgradeReceiveMessage>("upgrade");
            this.MessageManager.RegisterMessage<UpdateMetaReceiveMessage>("updatemeta");
            this.MessageManager.RegisterMessage<ShowKeyReceiveMessage>("show");
            this.MessageManager.RegisterMessage<HideKeyReceiveMessage>("hide");
            this.MessageManager.RegisterMessage<AddReceiveMessage>("add");
            this.MessageManager.RegisterMessage<LeftReceiveMessage>("left");
            this.MessageManager.RegisterMessage<MoveReceiveMessage>("m");
            this.MessageManager.RegisterMessage<FaceReceiveMessage>("face");
            this.MessageManager.RegisterMessage<PotionReceiveMessage>("p");
            this.MessageManager.RegisterMessage<CoinReceiveMessage>("c");
            this.MessageManager.RegisterMessage<CrownReceiveMessage>("k");
            this.MessageManager.RegisterMessage<SilverCrownReceiveMessage>("ks");
            this.MessageManager.RegisterMessage<MagicReceiveMessage>("w");
            this.MessageManager.RegisterMessage<LevelUpReceiveMessage>("levelup");
            this.MessageManager.RegisterMessage<GodModeReceiveMessage>("god");
            this.MessageManager.RegisterMessage<ModModeReceiveMessage>("mod");
            this.MessageManager.RegisterMessage<WootUpReceiveMessage>("wu");
            this.MessageManager.RegisterMessage<AllowPotionsReceiveMessage>("allowpotions");
            this.MessageManager.RegisterMessage<KillReceiveMessage>("kill");
            this.MessageManager.RegisterMessage<AccessReceiveMessage>("access");
            this.MessageManager.RegisterMessage<LostAccessReceiveMessage>("lostaccess");
            this.MessageManager.RegisterMessage<ResetReceiveMessage>("reset");
            this.MessageManager.RegisterMessage<TeleportEveryoneReceiveMessage>("tele");
            this.MessageManager.RegisterMessage<TeleportPlayerReceiveMessage>("teleport");
            this.MessageManager.RegisterMessage<SaveDoneReceiveMessage>("saved");
            this.MessageManager.RegisterMessage<ClearReceiveMessage>("clear");
            this.MessageManager.RegisterMessage<SayReceiveMessage>("say");
            this.MessageManager.RegisterMessage<SayOldReceiveMessage>("say_old");
            this.MessageManager.RegisterMessage<AutoTextReceiveMessage>("autotext");
            this.MessageManager.RegisterMessage<WriteReceiveMessage>("write");
            this.MessageManager.RegisterMessage<BlockPlaceReceiveMessage>("b");
            this.MessageManager.RegisterMessage<CoinDoorPlaceReceiveMessage>("bc");
            this.MessageManager.RegisterMessage<SoundPlaceReceiveMessage>("bs");
            this.MessageManager.RegisterMessage<RotatablePlaceReceiveMessage>("br");
            this.MessageManager.RegisterMessage<PortalPlaceReceiveMessage>("pt");
            this.MessageManager.RegisterMessage<WorldPortalPlaceReceiveMessage>("wp");
            this.MessageManager.RegisterMessage<LabelPlaceReceiveMessage>("lb");
            this.MessageManager.RegisterMessage<LabelPlaceReceiveMessage>("ts");
            this.MessageManager.RegisterMessage<GiveWizardReceiveMessage>("givewizard");
            this.MessageManager.RegisterMessage<GiveFireWizardReceiveMessage>("givewizard2");
            this.MessageManager.RegisterMessage<GiveDarkWizardReceiveMessage>("givedarkwizard");
            this.MessageManager.RegisterMessage<GiveWitchReceiveMessage>("givewitch");
            this.MessageManager.RegisterMessage<GiveGrinchReceiveMessage>("givegrinch");
            this.MessageManager.RegisterMessage<RefreshShopReceiveMessage>("refreshshop");

            // Bind OnMessage
            this.ConnectionPlatform.Connection.OnMessage += this.Connection_OnMessage;

            // Bind SendMessage event
            this.EventsPlatform.Event<SendMessage>().Bind(this.OnSendMessage);

            // Bind all SendMessage events
            this.EventsPlatform.Event<InitSendMessage>().Bind(this.OnAnySendMessage);
            this.EventsPlatform.Event<Init2SendMessage>().Bind(this.OnAnySendMessage);
            this.EventsPlatform.Event<BlockPlaceSendMessage>().Bind(this.OnAnySendMessage);
            this.EventsPlatform.Event<CoinDoorPlaceSendMessage>().Bind(this.OnAnySendMessage);
            this.EventsPlatform.Event<RotatablePlaceSendMessage>().Bind(this.OnAnySendMessage);
            this.EventsPlatform.Event<SoundPlaceSendMessage>().Bind(this.OnAnySendMessage);
            this.EventsPlatform.Event<PortalPlaceSendMessage>().Bind(this.OnAnySendMessage);
            this.EventsPlatform.Event<WorldPortalPlaceSendMessage>().Bind(this.OnAnySendMessage);
            this.EventsPlatform.Event<LabelPlaceSendMessage>().Bind(this.OnAnySendMessage);
            this.EventsPlatform.Event<CoinSendMessage>().Bind(this.OnAnySendMessage);
            this.EventsPlatform.Event<PressRedKeySendMessage>().Bind(this.OnAnySendMessage);
            this.EventsPlatform.Event<PressBlueKeySendMessage>().Bind(this.OnAnySendMessage);
            this.EventsPlatform.Event<PressGreenKeySendMessage>().Bind(this.OnAnySendMessage);
            this.EventsPlatform.Event<GetCrownSendMessage>().Bind(this.OnAnySendMessage);
            this.EventsPlatform.Event<TouchDiamondSendMessage>().Bind(this.OnAnySendMessage);
            this.EventsPlatform.Event<TouchCakeSendMessage>().Bind(this.OnAnySendMessage);
            this.EventsPlatform.Event<CompleteLevelSendMessage>().Bind(this.OnAnySendMessage);
            this.EventsPlatform.Event<GodModeSendMessage>().Bind(this.OnAnySendMessage);
            this.EventsPlatform.Event<ModModeSendMessage>().Bind(this.OnAnySendMessage);
            this.EventsPlatform.Event<MoveSendMessage>().Bind(this.OnAnySendMessage);
            this.EventsPlatform.Event<SaySendMessage>().Bind(this.OnAnySendMessage);
            this.EventsPlatform.Event<AutoSaySendMessage>().Bind(this.OnAnySendMessage);
            this.EventsPlatform.Event<AccessSendMessage>().Bind(this.OnAnySendMessage);
            this.EventsPlatform.Event<ChangeFaceSendMessage>().Bind(this.OnAnySendMessage);
            this.EventsPlatform.Event<SaveWorldSendMessage>().Bind(this.OnAnySendMessage);
            this.EventsPlatform.Event<ChangeWorldNameSendMessage>().Bind(this.OnAnySendMessage);
            this.EventsPlatform.Event<ChangeWorldEditKeySendMessage>().Bind(this.OnAnySendMessage);
            this.EventsPlatform.Event<ClearWorldSendMessage>().Bind(this.OnAnySendMessage);
            this.EventsPlatform.Event<KillWorldSendMessage>().Bind(this.OnAnySendMessage);
            this.EventsPlatform.Event<PotionSendMessage>().Bind(this.OnAnySendMessage);
            this.EventsPlatform.Event<AllowPotionsSendMessage>().Bind(this.OnAnySendMessage);
            this.EventsPlatform.Event<WootUpSendMessage>().Bind(this.OnAnySendMessage);
            this.EventsPlatform.Event<DeathSendMessage>().Bind(this.OnAnySendMessage);
            this.EventsPlatform.Event<CheckpointSendMessage>().Bind(this.OnAnySendMessage);
            this.EventsPlatform.Event<TouchPlayerSendMessage>().Bind(this.OnAnySendMessage);
        }

        private void Connection_OnMessage(object sender, Message e)
        {
            if (this.MessageManager.Contains(e.Type))
            {
                this.MessageManager[e.Type].Invoke(sender, e);
            }
            else
            {
                this.LogService.Log(LogPriority.Debug, "Received unregistered message with type: " + e.Type);
            }
        }

        private void OnAnySendMessage(object sender, SendMessage e)
        {
            this.EventsPlatform.Event<SendMessage>().Raise(sender, e);
        }

        private void OnSendMessage(object sender, SendMessage e)
        {
            this.ConnectionPlatform.Connection.Send(e.GetMessage());
        }
    }
}