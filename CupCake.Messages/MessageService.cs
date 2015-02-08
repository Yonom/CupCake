using System;
using CupCake.Core;
using CupCake.Core.Events;
using CupCake.Core.Log;
using CupCake.Messages.Receive;
using CupCake.Messages.Send;
using PlayerIOClient;

namespace CupCake.Messages
{
    public sealed class MessageService : CupCakeService
    {
        private bool _inited;
        public MessageManager MessageManager { get; private set; }

        protected override void Enable()
        {
            // Bind EnableComplete
            this.ServiceLoader.EnableComplete += this.ServiceLoader_EnableComplete;

            // Init MessageManager
            this.MessageManager = new MessageManager(this.Events);

            // Register some EE messages
            this.MessageManager.RegisterMessage<InitReceiveEvent>("init");
            this.MessageManager.RegisterMessage<InfoReceiveEvent>("info");
            this.MessageManager.RegisterMessage<UpgradeReceiveEvent>("upgrade");
            this.MessageManager.RegisterMessage<AllowPotionsReceiveEvent>("allowpotions");

            // Bind all SendEvent events
            this.Events.Bind<InitSendEvent>(this.OnAnySendEvent, EventPriority.Lowest);
            this.Events.Bind<Init2SendEvent>(this.OnAnySendEvent, EventPriority.Lowest);
            this.Events.Bind<BlockPlaceSendEvent>(this.OnAnySendEvent, EventPriority.Lowest);
            this.Events.Bind<CoinDoorPlaceSendEvent>(this.OnAnySendEvent, EventPriority.Lowest);
            this.Events.Bind<RotatablePlaceSendEvent>(this.OnAnySendEvent, EventPriority.Lowest);
            this.Events.Bind<SoundPlaceSendEvent>(this.OnAnySendEvent, EventPriority.Lowest);
            this.Events.Bind<PortalPlaceSendEvent>(this.OnAnySendEvent, EventPriority.Lowest);
            this.Events.Bind<WorldPortalPlaceSendEvent>(this.OnAnySendEvent, EventPriority.Lowest);
            this.Events.Bind<SignPlaceSendEvent>(this.OnAnySendEvent, EventPriority.Lowest);
            this.Events.Bind<LabelPlaceSendEvent>(this.OnAnySendEvent, EventPriority.Lowest);
            this.Events.Bind<PurpleDoorPlaceSendEvent>(this.OnAnySendEvent, EventPriority.Lowest);
            this.Events.Bind<DeathDoorPlaceSendEvent>(this.OnAnySendEvent, EventPriority.Lowest);
            this.Events.Bind<CoinSendEvent>(this.OnAnySendEvent, EventPriority.Lowest);
            this.Events.Bind<PressRedKeySendEvent>(this.OnAnySendEvent, EventPriority.Lowest);
            this.Events.Bind<PressBlueKeySendEvent>(this.OnAnySendEvent, EventPriority.Lowest);
            this.Events.Bind<PressGreenKeySendEvent>(this.OnAnySendEvent, EventPriority.Lowest);
            this.Events.Bind<PressCyanKeySendEvent>(this.OnAnySendEvent, EventPriority.Lowest);
            this.Events.Bind<PressMagentaKeySendEvent>(this.OnAnySendEvent, EventPriority.Lowest);
            this.Events.Bind<PressYellowKeySendEvent>(this.OnAnySendEvent, EventPriority.Lowest);
            this.Events.Bind<GetCrownSendEvent>(this.OnAnySendEvent, EventPriority.Lowest);
            this.Events.Bind<TouchDiamondSendEvent>(this.OnAnySendEvent, EventPriority.Lowest);
            this.Events.Bind<TouchCakeSendEvent>(this.OnAnySendEvent, EventPriority.Lowest);
            this.Events.Bind<CompleteLevelSendEvent>(this.OnAnySendEvent, EventPriority.Lowest);
            this.Events.Bind<GodModeSendEvent>(this.OnAnySendEvent, EventPriority.Lowest);
            this.Events.Bind<ModModeSendEvent>(this.OnAnySendEvent, EventPriority.Lowest);
            this.Events.Bind<MoveSendEvent>(this.OnAnySendEvent, EventPriority.Lowest);
            this.Events.Bind<SaySendEvent>(this.OnAnySendEvent, EventPriority.Lowest);
            this.Events.Bind<AutoSaySendEvent>(this.OnAnySendEvent, EventPriority.Lowest);
            this.Events.Bind<AccessSendEvent>(this.OnAnySendEvent, EventPriority.Lowest);
            this.Events.Bind<ChangeFaceSendEvent>(this.OnAnySendEvent, EventPriority.Lowest);
            this.Events.Bind<SaveWorldSendEvent>(this.OnAnySendEvent, EventPriority.Lowest);
            this.Events.Bind<ChangeWorldNameSendEvent>(this.OnAnySendEvent, EventPriority.Lowest);
            this.Events.Bind<ChangeWorldEditKeySendEvent>(this.OnAnySendEvent, EventPriority.Lowest);
            this.Events.Bind<ClearWorldSendEvent>(this.OnAnySendEvent, EventPriority.Lowest);
            this.Events.Bind<KillWorldSendEvent>(this.OnAnySendEvent, EventPriority.Lowest);
            this.Events.Bind<PotionSendEvent>(this.OnAnySendEvent, EventPriority.Lowest);
            this.Events.Bind<AllowPotionsSendEvent>(this.OnAnySendEvent, EventPriority.Lowest);
            this.Events.Bind<WootUpSendEvent>(this.OnAnySendEvent, EventPriority.Lowest);
            this.Events.Bind<DeathSendEvent>(this.OnAnySendEvent, EventPriority.Lowest);
            this.Events.Bind<CheckpointSendEvent>(this.OnAnySendEvent, EventPriority.Lowest);
            this.Events.Bind<TouchUserSendEvent>(this.OnAnySendEvent, EventPriority.Lowest);
            this.Events.Bind<ShowPurpleSendEvent>(this.OnAnySendEvent, EventPriority.Lowest);

            // Bind OnMessage
            this.ConnectionPlatform.Connection.OnMessage += this.Connection_OnMessage;
        }

        private void ServiceLoader_EnableComplete(object sender, EventArgs e)
        {
            // Send the init message
            this.Events.Raise(new InitSendEvent());
        }

        [EventListener(EventPriority.High)]
        private void OnInit(InitReceiveEvent e)
        {
            this._inited = true;

            // Register remaining messages
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
            this.MessageManager.RegisterMessage<GuardianModeReceiveEvent>("guardian");
            this.MessageManager.RegisterMessage<ModModeReceiveEvent>("mod");
            this.MessageManager.RegisterMessage<WootUpReceiveEvent>("wu");
            this.MessageManager.RegisterMessage<KillReceiveEvent>("kill");
            this.MessageManager.RegisterMessage<AccessReceiveEvent>("access");
            this.MessageManager.RegisterMessage<LostAccessReceiveEvent>("lostaccess");
            this.MessageManager.RegisterMessage<ResetReceiveEvent>("reset");
            this.MessageManager.RegisterMessage<TeleportEveryoneReceiveEvent>("tele");
            this.MessageManager.RegisterMessage<TeleportUserReceiveEvent>("teleport");
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
            this.MessageManager.RegisterMessage<SignPlaceReceiveEvent>("ts");
            this.MessageManager.RegisterMessage<GiveWizardReceiveEvent>("givewizard");
            this.MessageManager.RegisterMessage<GiveFireWizardReceiveEvent>("givewizard2");
            this.MessageManager.RegisterMessage<GiveDarkWizardReceiveEvent>("givedarkwizard");
            this.MessageManager.RegisterMessage<GiveWitchReceiveEvent>("givewitch");
            this.MessageManager.RegisterMessage<GiveGrinchReceiveEvent>("givegrinch");
            this.MessageManager.RegisterMessage<RefreshShopReceiveEvent>("refreshshop");

            // Send the init2 message
            this.Events.Raise(new Init2SendEvent());
        }

        private void Connection_OnMessage(object sender, Message e)
        {
            this.SynchronizePlatform.Do(() => this.HandleMessage(e));
        }

        private void HandleMessage(Message e)
        {
            IRegisteredMessage message;
            if (this.MessageManager.TryGetMessage(e.Type, out message))
            {
                message.Invoke(e);
            }
            else if (this._inited)
            {
                this.Logger.Log(LogPriority.Debug, "Received unregistered message with type: " + e.Type);
            }
        }

        private void OnAnySendEvent(object sender, SendEvent e)
        {
            this.Events.Raise(e);
        }

        [EventListener(EventPriority.Lowest)]
        private void OnSendEvent(SendEvent e)
        {
            if (!e.Cancelled)
            {
                this.ConnectionPlatform.Connection.Send(e.GetMessage());
            }
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                this.ConnectionPlatform.Connection.OnMessage -= this.Connection_OnMessage;
            }

            base.Dispose(disposing);
        }
    }
}