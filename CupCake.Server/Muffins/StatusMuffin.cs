using System;
using CupCake.Core;
using CupCake.HostAPI.Status;
using CupCake.Messages.Receive;
using CupCake.Players;

namespace CupCake.Server.Muffins
{
    public class StatusMuffin : CupCakeMuffin
    {
        private readonly StatusItem _onlinePlayers = new StatusItem("Online Players", "0");
        private readonly StatusItem _plays = new StatusItem("Plays", "0");
        private readonly StatusItem _totalWoots = new StatusItem("Total Woots", "0");
        private readonly StatusItem _woots = new StatusItem("Woots", "0");

        protected override void Enable()
        {
        }

        [EventListener]
        private void OnJoin(JoinPlayerEvent e)
        {
            this.UpdateOnline();
        }

        [EventListener]
        private void OnLeft(LeftPlayerEvent e)
        {
            this.UpdateOnline();
        }

        [EventListener]
        private void OnInitComplete(InitReceiveEvent e)
        {
            this.StatusService.Add(this._onlinePlayers);
            this.StatusService.Add(this._plays);
            this.StatusService.Add(this._woots);
            this.StatusService.Add(this._totalWoots);
        }

        [EventListener]
        private void OnInit(InitReceiveEvent e)
        {
            this.UpdateMeta(e);
        }

        [EventListener]
        private void OnUpdateMeta(UpdateMetaReceiveEvent e)
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