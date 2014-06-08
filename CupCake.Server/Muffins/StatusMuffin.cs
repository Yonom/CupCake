using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using CupCake.HostAPI.Status;
using CupCake.Messages.Receive;
using CupCake.Players;
using CupCake.Room;

namespace CupCake.Server.Muffins
{
    public class StatusMuffin : CupCakeMuffin
    {
        private readonly StatusItem _onlinePlayers = new StatusItem("Online Players", "0");
        private readonly StatusItem _plays = new StatusItem("Plays", "0");
        private readonly StatusItem _woots = new StatusItem("Woots", "0");
        private readonly StatusItem _totalWoots = new StatusItem("Total Woots", "0");

        protected override void Enable()
        {
            this.Events.Bind<InitReceiveEvent>(this.OnInit);
            this.Events.Bind<InitCompleteEvent>(this.OnInitComplete);
            this.Events.Bind<UpdateMetaReceiveEvent>(this.OnUpdateMeta);
            this.Events.Bind<AddPlayerEvent>(this.OnAdd);
            this.Events.Bind<LeftPlayerEvent>(this.OnLeft);
        }

        private void OnAdd(object sender, AddPlayerEvent e)
        {
            this.UpdateOnline();
        }

        private void OnLeft(object sender, LeftPlayerEvent e)
        {
            this.UpdateOnline();
        }

        private void OnInitComplete(object sender, InitCompleteEvent e)
        {
            this.StatusService.Add(_onlinePlayers);
            this.StatusService.Add(_plays);
            this.StatusService.Add(_woots);
            this.StatusService.Add(_totalWoots);

            this.UpdateOnline();
        }

        private void OnInit(object sender, InitReceiveEvent e)
        {
            this.UpdateMeta(e);
        }
        
        private void OnUpdateMeta(object sender, UpdateMetaReceiveEvent e)
        {
            this.UpdateMeta(e);
        }

        private void UpdateOnline()
        {
            this._onlinePlayers.Value = Convert.ToString(this.PlayerService.Count);
        }

        private void UpdateMeta(IMetadataReceiveMessage e)
        {
            this._plays.Value = Convert.ToString(e.Plays);
            this._woots.Value = Convert.ToString(e.CurrentWoots);
            this._totalWoots.Value = Convert.ToString(e.TotalWoots);
        }
    }
}
