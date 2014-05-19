using System;
using System.Collections.Generic;
using System.Linq;
using System.Timers;
using CupCake.Core.Services;
using CupCake.EE.Blocks;
using CupCake.EE.Messages.Send;

namespace CupCake.Chat.Services
{
    public class ChatService : CupCakeService
    {
        private readonly Queue<SaySendMessage> _myChatQueue = new Queue<SaySendMessage>();
        private readonly List<string> _myHistoryList = new List<string>();
        private Timer _mySendTimer;

        public IChatSyntaxProvider ChatSyntaxProvider { get; set; }

        protected override void Enable()
        {
            this.ChatSyntaxProvider = new BasicChatSyntaxProvider();

            this._mySendTimer = new Timer(700);
            this._mySendTimer.Elapsed += this.SendTimer_Elapsed;
            this._mySendTimer.Start();
        }

        private void SendTimer_Elapsed(object sender, ElapsedEventArgs elapsedEventArgs)
        {
            this.DoSendTick();
        }

        private void DoSendTick()
        {
            lock (this._myChatQueue)
            {
                if (this._myChatQueue.Count > 0)
                {
                    this.EventsPlatform.Event<SaySendMessage>().Raise(this, this._myChatQueue.Dequeue());
                }
                else
                {
                    this._mySendTimer.Stop();
                }
            }
        }

        private bool CheckHistory(string str)
        {
            return this._myHistoryList.Count(str.Equals) >= 4;
        }

        private void SendChat(string msg)
        {
            // There is no speed limit on commands
            if (msg.StartsWith("/", StringComparison.Ordinal))
            {
                this.EventsPlatform.Event<SaySendMessage>().Raise(this, new SaySendMessage(msg.Substring(0, 80)));
                return;
            }

            // Dont send the same thing more than 3 times
            if (this.CheckHistory(msg))
            {
                this.SendChat("." + msg);
            }

            lock (this._myHistoryList)
            {
                this._myHistoryList.Add(msg);
                if (this._myHistoryList.Count > 10)
                {
                    this._myHistoryList.RemoveAt(0);
                }
            }

            // Queue the message and chop it into 80 char parts
            lock (this._myChatQueue)
            {
                for (int i = 0; i <= msg.Length; i += 80)
                {
                    int left = msg.Length - i;
                    this._myChatQueue.Enqueue(left >= 80
                        ? new SaySendMessage(msg.Substring(i, 80))
                        : new SaySendMessage(msg.Substring(i, left)));
                }
            }

            // Init Timer
            if (!this._mySendTimer.Enabled)
            {
                this.DoSendTick();
                this._mySendTimer.Start();
            }
        }

        public void Chat(string msg, string chatName)
        {
            this.SendChat(this.ChatSyntaxProvider.ApplyChatSyntax(msg, chatName));
        }

        public void Send(string msg)
        {
            this.SendChat(msg);
        }

        public void Reply(string username, string chatName, string msg)
        {
            this.SendChat(this.ChatSyntaxProvider.ApplyReplySyntax(msg, chatName, username));
        }

        public void GiveEdit(string username)
        {
            this.SendChat("/giveedit " + username);
        }

        public void RemoveEdit(string username)
        {
            this.SendChat("/removeedit " + username);
        }

        public void Teleport(string username)
        {
            this.SendChat("/teleport " + username);
        }

        public void Teleport(string username, int x, int y)
        {
            this.SendChat("/teleport " + username + " " + x + " " + y);
        }

        public void Kick(string chatName, string username)
        {
            this.Kick(chatName, username, String.Empty);
        }

        public void Kick(string chatName, string username, string reason)
        {
            this.SendChat(this.ChatSyntaxProvider.ApplyKickSyntax(chatName, username, reason));
        }

        public void KickGuests()
        {
            this.SendChat("/kickguests");
        }

        public void Kill(string username)
        {
            this.SendChat("/kill " + username);
        }

        public void KillAll()
        {
            this.SendChat("/killemall");
        }

        public void Reset()
        {
            this.SendChat("/reset");
        }

        public void Respawn()
        {
            this.SendChat("/respawn");
        }

        public void RespawnAll()
        {
            this.SendChat("/respawnall");
        }

        public void PotionsOn(params string[] potions)
        {
            this.SendChat("/potionson  " + string.Join(" ", potions));
        }

        public void PotionsOn(params int[] potions)
        {
            this.SendChat("/potionson  " + string.Join(" ", potions));
        }

        public void PotionsOn(params Potion[] potions)
        {
            var p = new int[potions.Length];
            for (int i = potions.Length - 1; i >= 0; i += -1)
            {
                p[i] = Convert.ToInt32(potions[i]);
            }

            this.SendChat("/potionson  " + string.Join(" ", p));
        }

        public void PotionsOff(params string[] potions)
        {
            this.SendChat("/potionsoff  " + string.Join(" ", potions));
        }

        public void PotionsOff(params int[] potions)
        {
            this.SendChat("/potionsoff  " + string.Join(" ", potions));
        }

        public void PotionsOff(params Potion[] potions)
        {
            var p = new int[potions.Length];
            for (int i = potions.Length - 1; i >= 0; i += -1)
            {
                p[i] = Convert.ToInt32(potions[i]);
            }

            this.SendChat("/potionsoff  " + string.Join(" ", p));
        }

        public void ChangeVisibility(bool visible)
        {
            this.SendChat("/visible " + visible);
        }

        public void LoadLevel()
        {
            this.SendChat("/loadlevel");
        }

        public override void Dispose()
        {
            this._myHistoryList.Clear();
            this._myChatQueue.Clear();
            this._mySendTimer.Enabled = false;
        }
    }
}