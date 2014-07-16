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
            // Init MessageManager
            MessageManager = new MessageManager(Events);

            // Register some EE messages
            MessageManager.RegisterMessage<InitReceiveEvent>("init");
            MessageManager.RegisterMessage<InfoReceiveEvent>("info");
            MessageManager.RegisterMessage<UpgradeReceiveEvent>("upgrade");
            MessageManager.RegisterMessage<AllowPotionsReceiveEvent>("allowpotions");

            // Bind Init receive message
            Events.Bind<InitReceiveEvent>(OnInit, EventPriority.High);

            // Bind OnMessage
            ConnectionPlatform.Connection.OnMessage += Connection_OnMessage;

            // Bind SendEvent event
            Events.Bind<SendEvent>(OnSendEvent, EventPriority.Lowest);

            // Bind all SendEvent events
            Events.Bind<InitSendEvent>(OnAnySendEvent, EventPriority.Lowest);
            Events.Bind<Init2SendEvent>(OnAnySendEvent, EventPriority.Lowest);
            Events.Bind<BlockPlaceSendEvent>(OnAnySendEvent, EventPriority.Lowest);
            Events.Bind<CoinDoorPlaceSendEvent>(OnAnySendEvent, EventPriority.Lowest);
            Events.Bind<RotatablePlaceSendEvent>(OnAnySendEvent, EventPriority.Lowest);
            Events.Bind<SoundPlaceSendEvent>(OnAnySendEvent, EventPriority.Lowest);
            Events.Bind<PortalPlaceSendEvent>(OnAnySendEvent, EventPriority.Lowest);
            Events.Bind<WorldPortalPlaceSendEvent>(OnAnySendEvent, EventPriority.Lowest);
            Events.Bind<LabelPlaceSendEvent>(OnAnySendEvent, EventPriority.Lowest);
            Events.Bind<CoinSendEvent>(OnAnySendEvent, EventPriority.Lowest);
            Events.Bind<PressRedKeySendEvent>(OnAnySendEvent, EventPriority.Lowest);
            Events.Bind<PressBlueKeySendEvent>(OnAnySendEvent, EventPriority.Lowest);
            Events.Bind<PressGreenKeySendEvent>(OnAnySendEvent, EventPriority.Lowest);
            Events.Bind<GetCrownSendEvent>(OnAnySendEvent, EventPriority.Lowest);
            Events.Bind<TouchDiamondSendEvent>(OnAnySendEvent, EventPriority.Lowest);
            Events.Bind<TouchCakeSendEvent>(OnAnySendEvent, EventPriority.Lowest);
            Events.Bind<CompleteLevelSendEvent>(OnAnySendEvent, EventPriority.Lowest);
            Events.Bind<GodModeSendEvent>(OnAnySendEvent, EventPriority.Lowest);
            Events.Bind<ModModeSendEvent>(OnAnySendEvent, EventPriority.Lowest);
            Events.Bind<MoveSendEvent>(OnAnySendEvent, EventPriority.Lowest);
            Events.Bind<SaySendEvent>(OnAnySendEvent, EventPriority.Lowest);
            Events.Bind<AutoSaySendEvent>(OnAnySendEvent, EventPriority.Lowest);
            Events.Bind<AccessSendEvent>(OnAnySendEvent, EventPriority.Lowest);
            Events.Bind<ChangeFaceSendEvent>(OnAnySendEvent, EventPriority.Lowest);
            Events.Bind<SaveWorldSendEvent>(OnAnySendEvent, EventPriority.Lowest);
            Events.Bind<ChangeWorldNameSendEvent>(OnAnySendEvent, EventPriority.Lowest);
            Events.Bind<ChangeWorldEditKeySendEvent>(OnAnySendEvent, EventPriority.Lowest);
            Events.Bind<ClearWorldSendEvent>(OnAnySendEvent, EventPriority.Lowest);
            Events.Bind<KillWorldSendEvent>(OnAnySendEvent, EventPriority.Lowest);
            Events.Bind<PotionSendEvent>(OnAnySendEvent, EventPriority.Lowest);
            Events.Bind<AllowPotionsSendEvent>(OnAnySendEvent, EventPriority.Lowest);
            Events.Bind<WootUpSendEvent>(OnAnySendEvent, EventPriority.Lowest);
            Events.Bind<DeathSendEvent>(OnAnySendEvent, EventPriority.Lowest);
            Events.Bind<CheckpointSendEvent>(OnAnySendEvent, EventPriority.Lowest);
            Events.Bind<TouchUserSendEvent>(OnAnySendEvent, EventPriority.Lowest);
            Events.Bind<ShowPurpleSendEvent>(OnAnySendEvent, EventPriority.Lowest);
        }

        private void OnInit(object sender, InitReceiveEvent e)
        {
            _inited = true;

            // Register remaining messages
            MessageManager.RegisterMessage<UpdateMetaReceiveEvent>("updatemeta");
            MessageManager.RegisterMessage<ShowKeyReceiveEvent>("show");
            MessageManager.RegisterMessage<HideKeyReceiveEvent>("hide");
            MessageManager.RegisterMessage<AddReceiveEvent>("add");
            MessageManager.RegisterMessage<LeftReceiveEvent>("left");
            MessageManager.RegisterMessage<MoveReceiveEvent>("m");
            MessageManager.RegisterMessage<FaceReceiveEvent>("face");
            MessageManager.RegisterMessage<PotionReceiveEvent>("p");
            MessageManager.RegisterMessage<CoinReceiveEvent>("c");
            MessageManager.RegisterMessage<CrownReceiveEvent>("k");
            MessageManager.RegisterMessage<SilverCrownReceiveEvent>("ks");
            MessageManager.RegisterMessage<MagicReceiveEvent>("w");
            MessageManager.RegisterMessage<LevelUpReceiveEvent>("levelup");
            MessageManager.RegisterMessage<GodModeReceiveEvent>("god");
            MessageManager.RegisterMessage<ModModeReceiveEvent>("mod");
            MessageManager.RegisterMessage<WootUpReceiveEvent>("wu");
            MessageManager.RegisterMessage<KillReceiveEvent>("kill");
            MessageManager.RegisterMessage<AccessReceiveEvent>("access");
            MessageManager.RegisterMessage<LostAccessReceiveEvent>("lostaccess");
            MessageManager.RegisterMessage<ResetReceiveEvent>("reset");
            MessageManager.RegisterMessage<TeleportEveryoneReceiveEvent>("tele");
            MessageManager.RegisterMessage<TeleportUserReceiveEvent>("teleport");
            MessageManager.RegisterMessage<SaveDoneReceiveEvent>("saved");
            MessageManager.RegisterMessage<ClearReceiveEvent>("clear");
            MessageManager.RegisterMessage<SayReceiveEvent>("say");
            MessageManager.RegisterMessage<SayOldReceiveEvent>("say_old");
            MessageManager.RegisterMessage<AutoTextReceiveEvent>("autotext");
            MessageManager.RegisterMessage<WriteReceiveEvent>("write");
            MessageManager.RegisterMessage<BlockPlaceReceiveEvent>("b");
            MessageManager.RegisterMessage<CoinDoorPlaceReceiveEvent>("bc");
            MessageManager.RegisterMessage<SoundPlaceReceiveEvent>("bs");
            MessageManager.RegisterMessage<RotatablePlaceReceiveEvent>("br");
            MessageManager.RegisterMessage<PortalPlaceReceiveEvent>("pt");
            MessageManager.RegisterMessage<WorldPortalPlaceReceiveEvent>("wp");
            MessageManager.RegisterMessage<LabelPlaceReceiveEvent>("lb");
            MessageManager.RegisterMessage<LabelPlaceReceiveEvent>("ts");
            MessageManager.RegisterMessage<GiveWizardReceiveEvent>("givewizard");
            MessageManager.RegisterMessage<GiveFireWizardReceiveEvent>("givewizard2");
            MessageManager.RegisterMessage<GiveDarkWizardReceiveEvent>("givedarkwizard");
            MessageManager.RegisterMessage<GiveWitchReceiveEvent>("givewitch");
            MessageManager.RegisterMessage<GiveGrinchReceiveEvent>("givegrinch");
            MessageManager.RegisterMessage<RefreshShopReceiveEvent>("refreshshop");
        }

        private void Connection_OnMessage(object sender, Message e)
        {
            SynchronizePlatform.Do(() => HandleMessage(e));
        }

        private void HandleMessage(Message e)
        {
            IRegisteredMessage message;
            if (MessageManager.TryGetMessage(e.Type, out message))
            {
                message.Invoke(e);
            }
            else if (_inited)
            {
                Logger.Log(LogPriority.Debug, "Received unregistered message with type: " + e.Type);
            }
        }

        private void OnAnySendEvent(object sender, SendEvent e)
        {
            Events.Raise(e);
        }

        private void OnSendEvent(object sender, SendEvent e)
        {
            if (!e.Cancelled)
            {
                ConnectionPlatform.Connection.Send(e.GetMessage());
            }
        }
    }
}