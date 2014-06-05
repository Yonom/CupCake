using System;
using CupCake.Core;
using CupCake.Messages.Blocks;
using CupCake.Messages.Receive;
using CupCake.Messages.Send;

namespace CupCake.Potions
{
    public class PotionService : CupCakeService
    {
        public int RedAuraPotionCount { get; private set; }
        public int BlueAuraPotionCount { get; private set; }
        public int YellowAuraPotionCount { get; private set; }
        public int GreenAuraPotionCount { get; private set; }
        public int JumpPotionCount { get; private set; }
        public int FirePotionCount { get; private set; }
        public int CursePotionCount { get; private set; }
        public int ProtectionPotionCount { get; private set; }
        public int ZombiePotionCount { get; private set; }
        public int RespawnPotionCount { get; private set; }
        public int LevitationPotionCount { get; private set; }
        public int FlauntPotionCount { get; private set; }
        public int SolitudePotionCount { get; private set; }

        protected override void Enable()
        {
            this.Events.Bind<InitReceiveEvent>(this.OnInit);
        }

        private void OnInit(object sender, InitReceiveEvent e)
        {
            uint startNum = 0;
            for (int i = Convert.ToInt32(e.PlayerIOMessage.Count - 1u); i >= 0; i += -1)
            {
                if (e.PlayerIOMessage[Convert.ToUInt32(i)] as string != null &&
                    e.PlayerIOMessage.GetString(Convert.ToUInt32(i)) == "pe")
                {
                    startNum = Convert.ToUInt32(i - 1);
                }
            }

            uint pointer = startNum;
            while (e.PlayerIOMessage[pointer] as string == null || e.PlayerIOMessage.GetString(pointer) != "ps")
            {
                switch ((Potion)e.PlayerIOMessage.GetInteger(pointer - 1u))
                {
                    case Potion.RedAura:
                        this.RedAuraPotionCount = e.PlayerIOMessage.GetInteger(pointer);
                        break;
                    case Potion.BlueAura:
                        this.BlueAuraPotionCount = e.PlayerIOMessage.GetInteger(pointer);
                        break;
                    case Potion.YellowAura:
                        this.YellowAuraPotionCount = e.PlayerIOMessage.GetInteger(pointer);
                        break;
                    case Potion.GreenAura:
                        this.GreenAuraPotionCount = e.PlayerIOMessage.GetInteger(pointer);
                        break;
                    case Potion.Jump:
                        this.JumpPotionCount = e.PlayerIOMessage.GetInteger(pointer);
                        break;
                    case Potion.Fire:
                        this.FirePotionCount = e.PlayerIOMessage.GetInteger(pointer);
                        break;
                    case Potion.Curse:
                        this.CursePotionCount = e.PlayerIOMessage.GetInteger(pointer);
                        break;
                    case Potion.Protection:
                        this.ProtectionPotionCount = e.PlayerIOMessage.GetInteger(pointer);
                        break;
                    case Potion.Zombie:
                        this.ZombiePotionCount = e.PlayerIOMessage.GetInteger(pointer);
                        break;
                    case Potion.Respawn:
                        this.RespawnPotionCount = e.PlayerIOMessage.GetInteger(pointer);
                        break;
                    case Potion.Levitation:
                        this.LevitationPotionCount = e.PlayerIOMessage.GetInteger(pointer);
                        break;
                    case Potion.Flaunt:
                        this.FlauntPotionCount = e.PlayerIOMessage.GetInteger(pointer);
                        break;
                    case Potion.Solitude:
                        this.SolitudePotionCount = e.PlayerIOMessage.GetInteger(pointer);
                        break;
                }
                pointer -= 2u;
            }
        }

        public void UsePotion(Potion pot)
        {
            this.Events.Raise(new PotionSendEvent(pot));
        }
    }
}