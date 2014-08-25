using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Timers;
using CupCake.Core;
using CupCake.Messages.Send;
using CupCake.Messages.User;

namespace CupCake.Chat
{
    public sealed class ChatService : CupCakeService
    {
        private readonly ConcurrentQueue<SaySendEvent> _myChatQueue = new ConcurrentQueue<SaySendEvent>();
        private readonly List<string> _myHistoryList = new List<string>();
        private Timer _mySendTimer;

        public IChatSyntaxProvider SyntaxProvider { get; set; }

        protected override void Enable()
        {
            this.SyntaxProvider = new CupCakeChatSyntaxProvider();

            this._mySendTimer = new Timer(700);
            this._mySendTimer.Elapsed += this.SendTimer_Elapsed;
        }

        private void SendTimer_Elapsed(object sender, ElapsedEventArgs elapsedEventArgs)
        {
            this.DoSendTick();
        }

        private void DoSendTick()
        {
            SaySendEvent sayEvent;
            if (this._myChatQueue.TryDequeue(out sayEvent))
            {
                this.Events.Raise(sayEvent);
            }
            else
            {
                this._mySendTimer.Stop();
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
                SaySendEvent e = msg.Length >= 80
                    ? new SaySendEvent(msg.Substring(0, 80))
                    : new SaySendEvent(msg.Substring(0, msg.Length));
                this.Events.Raise(e);
                return;
            }

            // Dont send the same thing more than 3 times
            if (this.CheckHistory(msg))
            {
                this.SendChat("." + msg);
                return;
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
            for (int i = 0; i <= msg.Length; i += 80)
            {
                int left = msg.Length - i;
                this._myChatQueue.Enqueue(left >= 80
                    ? new SaySendEvent(msg.Substring(i, 80))
                    : new SaySendEvent(msg.Substring(i, left)));
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
            this.SendChat(this.SyntaxProvider.ApplyChatSyntax(msg, chatName));
        }

        public void Send(string msg)
        {
            this.SendChat(msg);
        }

        public void Reply(string username, string chatName, string msg)
        {
            this.SendChat(this.SyntaxProvider.ApplyReplySyntax(msg, chatName, username));
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
            this.SendChat(this.SyntaxProvider.ApplyKickSyntax(chatName, username, reason));
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

        public void Mute(string username)
        {
            this.SendChat("/mute " + username);
        }

        public void Unmute(string username)
        {
            this.SendChat("/unmute " + username);
        }

        public void ReportAbuse(string username, string reason)
        {
            this.SendChat("/reportabuse " + username + " " + reason);
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
                p[i] = (int)potions[i];
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
                p[i] = (int)potions[i];
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

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                this._mySendTimer.Elapsed -= this.SendTimer_Elapsed;
                this._mySendTimer.Dispose();
            }

            base.Dispose(disposing);
        }
    }
}