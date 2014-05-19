﻿using System;
using System.Collections.Generic;
using System.Text;
using CupCake.Chat.Services;
using CupCake.EE.Blocks;

namespace CupCake.API
{
    public class Chatter
    {
        public ChatService ChatService { get; set; }
        public string Name { get; set; }

        public Chatter(ChatService chatService, string name)
        {
            this.ChatService = chatService;
            this.Name = name;
        }

        public void Chat(string msg)
        {
            this.ChatService.Chat(msg, this.Name);
        }

        public void Send(string msg)
        {
            this.ChatService.Send(msg);
        }

        public void Reply(string username, string msg)
        {
            this.ChatService.Reply(username, Name, msg);
        }

        public void GiveEdit(string username)
        {
            this.ChatService.GiveEdit(username);
        }

        public void RemoveEdit(string username)
        {
            this.ChatService.RemoveEdit(username);
        }

        public void Teleport(string username)
        {
            this.ChatService.Teleport(username);
        }

        public void Teleport(string username, int x, int y)
        {
            this.ChatService.Teleport(username, x, y);
        }

        public void Kick(string username)
        {
            this.ChatService.Kick(Name, username);
        }

        public void Kick(string username, string reason)
        {
            this.ChatService.Kick(Name, username, reason);
        }

        public void KickGuests()
        {
            this.ChatService.KickGuests();
        }

        public void Kill(string username)
        {
            this.ChatService.Kill(username);
        }

        public void KillAll()
        {
            this.ChatService.KillAll();
        }

        public void Reset()
        {
            this.ChatService.Reset();
        }

        public void Respawn()
        {
            this.ChatService.Respawn();
        }

        public void RespawnAll()
        {
            this.ChatService.RespawnAll();
        }

        public void PotionsOn(params string[] potions)
        {
            this.ChatService.PotionsOn(potions);
        }

        public void PotionsOn(params int[] potions)
        {
            this.ChatService.PotionsOn(potions);
        }

        public void PotionsOn(params Potion[] potions)
        {
            this.ChatService.PotionsOn(potions);
        }

        public void PotionsOff(params string[] potions)
        {
            this.ChatService.PotionsOff(potions);
        }

        public void PotionsOff(params int[] potions)
        {
            this.ChatService.PotionsOff(potions);
        }

        public void PotionsOff(params Potion[] potions)
        {
            this.ChatService.PotionsOff(potions);
        }

        public void ChangeVisibility(bool visible)
        {
            this.ChatService.ChangeVisibility(visible);
        }

        public void LoadLevel()
        {
            this.ChatService.LoadLevel();
        }
    }
}