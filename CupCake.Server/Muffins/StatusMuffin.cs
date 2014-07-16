using System;
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
            Events.Bind<InitReceiveEvent>(OnInit);
            Events.Bind<InitReceiveEvent>(OnInitComplete);
            Events.Bind<UpdateMetaReceiveEvent>(OnUpdateMeta);
            Events.Bind<JoinPlayerEvent>(OnJoin);
            Events.Bind<LeftPlayerEvent>(OnLeft);
        }

        private void OnJoin(object sender, JoinPlayerEvent e)
        {
            UpdateOnline();
        }

        private void OnLeft(object sender, LeftPlayerEvent e)
        {
            UpdateOnline();
        }

        private void OnInitComplete(object sender, InitReceiveEvent e)
        {
            StatusService.Add(_onlinePlayers);
            StatusService.Add(_plays);
            StatusService.Add(_woots);
            StatusService.Add(_totalWoots);
        }

        private void OnInit(object sender, InitReceiveEvent e)
        {
            UpdateMeta(e);
        }

        private void OnUpdateMeta(object sender, UpdateMetaReceiveEvent e)
        {
            UpdateMeta(e);
        }

        private void UpdateOnline()
        {
            _onlinePlayers.Value = Convert.ToString(PlayerService.Count);
        }

        private void UpdateMeta(IMetadataReceiveMessage e)
        {
            _plays.Value = Convert.ToString(e.Plays);
            _woots.Value = Convert.ToString(e.CurrentWoots);
            _totalWoots.Value = Convert.ToString(e.TotalWoots);
        }
    }
}