using System.Threading;
using CupCake.API.Muffins;
using CupCake.Core.Log;
using CupCake.EE.Events.Receive;
using CupCake.EE.Events.Send;
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
            this.MessageManager = new MessageManager(this.Events);

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
            this.Events.Bind<SendEvent>(this.OnSendEvent);

            // Bind all SendEvent events
            this.Events.Bind<InitSendEvent>(this.OnAnySendEvent);
            this.Events.Bind<Init2SendEvent>(this.OnAnySendEvent);
            this.Events.Bind<BlockPlaceSendEvent>(this.OnAnySendEvent);
            this.Events.Bind<CoinDoorPlaceSendEvent>(this.OnAnySendEvent);
            this.Events.Bind<RotatablePlaceSendEvent>(this.OnAnySendEvent);
            this.Events.Bind<SoundPlaceSendEvent>(this.OnAnySendEvent);
            this.Events.Bind<PortalPlaceSendEvent>(this.OnAnySendEvent);
            this.Events.Bind<WorldPortalPlaceSendEvent>(this.OnAnySendEvent);
            this.Events.Bind<LabelPlaceSendEvent>(this.OnAnySendEvent);
            this.Events.Bind<CoinSendEvent>(this.OnAnySendEvent);
            this.Events.Bind<PressRedKeySendEvent>(this.OnAnySendEvent);
            this.Events.Bind<PressBlueKeySendEvent>(this.OnAnySendEvent);
            this.Events.Bind<PressGreenKeySendEvent>(this.OnAnySendEvent);
            this.Events.Bind<GetCrownSendEvent>(this.OnAnySendEvent);
            this.Events.Bind<TouchDiamondSendEvent>(this.OnAnySendEvent);
            this.Events.Bind<TouchCakeSendEvent>(this.OnAnySendEvent);
            this.Events.Bind<CompleteLevelSendEvent>(this.OnAnySendEvent);
            this.Events.Bind<GodModeSendEvent>(this.OnAnySendEvent);
            this.Events.Bind<ModModeSendEvent>(this.OnAnySendEvent);
            this.Events.Bind<MoveSendEvent>(this.OnAnySendEvent);
            this.Events.Bind<SaySendEvent>(this.OnAnySendEvent);
            this.Events.Bind<AutoSaySendEvent>(this.OnAnySendEvent);
            this.Events.Bind<AccessSendEvent>(this.OnAnySendEvent);
            this.Events.Bind<ChangeFaceSendEvent>(this.OnAnySendEvent);
            this.Events.Bind<SaveWorldSendEvent>(this.OnAnySendEvent);
            this.Events.Bind<ChangeWorldNameSendEvent>(this.OnAnySendEvent);
            this.Events.Bind<ChangeWorldEditKeySendEvent>(this.OnAnySendEvent);
            this.Events.Bind<ClearWorldSendEvent>(this.OnAnySendEvent);
            this.Events.Bind<KillWorldSendEvent>(this.OnAnySendEvent);
            this.Events.Bind<PotionSendEvent>(this.OnAnySendEvent);
            this.Events.Bind<AllowPotionsSendEvent>(this.OnAnySendEvent);
            this.Events.Bind<WootUpSendEvent>(this.OnAnySendEvent);
            this.Events.Bind<DeathSendEvent>(this.OnAnySendEvent);
            this.Events.Bind<CheckpointSendEvent>(this.OnAnySendEvent);
            this.Events.Bind<TouchUserSendEvent>(this.OnAnySendEvent);
        }

        private void Connection_OnMessage(object sender, Message e)
        {
            ThreadPool.QueueUserWorkItem(this.ThreadPoolCallback, e);
        }

        private void ThreadPoolCallback(object state)
        {
            this.HandleMessage((Message)state);
        }

        private void HandleMessage(Message e)
        {
            IRegisteredMessage message;
            if (this.MessageManager.TryGetMessage(e.Type, out message))
            {
                message.Invoke(e);
            }
            else
            {
                this.Logger.Log(LogPriority.Debug, "Received unregistered message with type: " + e.Type);
            }
        }

        private void OnAnySendEvent(object sender, SendEvent e)
        {
            this.Events.Raise(e);
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