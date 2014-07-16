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
            SyntaxProvider = new BasicChatSyntaxProvider();

            _mySendTimer = new Timer(700);
            _mySendTimer.Elapsed += SendTimer_Elapsed;
            _mySendTimer.Start();
        }

        private void SendTimer_Elapsed(object sender, ElapsedEventArgs elapsedEventArgs)
        {
            DoSendTick();
        }

        private void DoSendTick()
        {
            SaySendEvent sayEvent;
            if (_myChatQueue.TryDequeue(out sayEvent))
            {
                Events.Raise(sayEvent);
            }
            else
            {
                _mySendTimer.Stop();
            }
        }

        private bool CheckHistory(string str)
        {
            return _myHistoryList.Count(str.Equals) >= 4;
        }

        private void SendChat(string msg)
        {
            // There is no speed limit on commands
            if (msg.StartsWith("/", StringComparison.Ordinal))
            {
                SaySendEvent e = msg.Length >= 80
                    ? new SaySendEvent(msg.Substring(0, 80))
                    : new SaySendEvent(msg.Substring(0, msg.Length));
                Events.Raise(e);
                return;
            }

            // Dont send the same thing more than 3 times
            if (CheckHistory(msg))
            {
                SendChat("." + msg);
            }

            lock (_myHistoryList)
            {
                _myHistoryList.Add(msg);
                if (_myHistoryList.Count > 10)
                {
                    _myHistoryList.RemoveAt(0);
                }
            }

            // Queue the message and chop it into 80 char parts
            for (int i = 0; i <= msg.Length; i += 80)
            {
                int left = msg.Length - i;
                _myChatQueue.Enqueue(left >= 80
                    ? new SaySendEvent(msg.Substring(i, 80))
                    : new SaySendEvent(msg.Substring(i, left)));
            }

            // Init Timer
            if (!_mySendTimer.Enabled)
            {
                DoSendTick();
                _mySendTimer.Start();
            }
        }

        public void Chat(string msg, string chatName)
        {
            SendChat(SyntaxProvider.ApplyChatSyntax(msg, chatName));
        }

        public void Send(string msg)
        {
            SendChat(msg);
        }

        public void Reply(string username, string chatName, string msg)
        {
            SendChat(SyntaxProvider.ApplyReplySyntax(msg, chatName, username));
        }

        public void GiveEdit(string username)
        {
            SendChat("/giveedit " + username);
        }

        public void RemoveEdit(string username)
        {
            SendChat("/removeedit " + username);
        }

        public void Teleport(string username)
        {
            SendChat("/teleport " + username);
        }

        public void Teleport(string username, int x, int y)
        {
            SendChat("/teleport " + username + " " + x + " " + y);
        }

        public void Kick(string chatName, string username)
        {
            Kick(chatName, username, String.Empty);
        }

        public void Kick(string chatName, string username, string reason)
        {
            SendChat(SyntaxProvider.ApplyKickSyntax(chatName, username, reason));
        }

        public void KickGuests()
        {
            SendChat("/kickguests");
        }

        public void Kill(string username)
        {
            SendChat("/kill " + username);
        }

        public void KillAll()
        {
            SendChat("/killemall");
        }

        public void Mute(string username)
        {
            SendChat("/mute " + username);
        }

        public void Unmute(string username)
        {
            SendChat("/unmute " + username);
        }

        public void ReportAbuse(string username, string reason)
        {
            SendChat("/reportabuse " + username + " " + reason);
        }

        public void Reset()
        {
            SendChat("/reset");
        }

        public void Respawn()
        {
            SendChat("/respawn");
        }

        public void RespawnAll()
        {
            SendChat("/respawnall");
        }

        public void PotionsOn(params string[] potions)
        {
            SendChat("/potionson  " + string.Join(" ", potions));
        }

        public void PotionsOn(params int[] potions)
        {
            SendChat("/potionson  " + string.Join(" ", potions));
        }

        public void PotionsOn(params Potion[] potions)
        {
            var p = new int[potions.Length];
            for (int i = potions.Length - 1; i >= 0; i += -1)
            {
                p[i] = (int) potions[i];
            }

            SendChat("/potionson  " + string.Join(" ", p));
        }

        public void PotionsOff(params string[] potions)
        {
            SendChat("/potionsoff  " + string.Join(" ", potions));
        }

        public void PotionsOff(params int[] potions)
        {
            SendChat("/potionsoff  " + string.Join(" ", potions));
        }

        public void PotionsOff(params Potion[] potions)
        {
            var p = new int[potions.Length];
            for (int i = potions.Length - 1; i >= 0; i += -1)
            {
                p[i] = (int) potions[i];
            }

            SendChat("/potionsoff  " + string.Join(" ", p));
        }

        public void ChangeVisibility(bool visible)
        {
            SendChat("/visible " + visible);
        }

        public void LoadLevel()
        {
            SendChat("/loadlevel");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _mySendTimer.Dispose();
            }
        }
    }
}