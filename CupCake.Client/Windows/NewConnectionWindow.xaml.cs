using System;
using System.Configuration;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using CupCake.Client.Settings;
using CupCake.Protocol;

namespace CupCake.Client.Windows
{
    /// <summary>
    /// Interaction logic for NewConnectionWindow.xaml
    /// </summary>
    public partial class NewConnectionWindow
    {
        private readonly RecentWorld _recentWorld;
        private readonly ClientHandle _handle;
        
        public NewConnectionWindow(ClientHandle handle, RecentWorld recentWorld)
        {
            this._recentWorld = recentWorld;
            InitializeComponent();

            this._handle = handle;
            this._handle.ReceiveClose += this._handle_ReceiveClose;

            this.Closed += this.NewConnectionWindow_Closed;

            foreach (var profile in SettingsManager.Settings.Profiles.OrderBy(v => v.Id))
            {
                var item = new TextBlock(new Run(profile.Name)) {Tag = profile};
                this.ProfileComboBox.Items.Add(item);

                if (recentWorld.Profile == profile.Id)
                    this.ProfileComboBox.SelectedItem = item;
            }

            foreach (var account in SettingsManager.Settings.Accounts.OrderBy(v => v.Id))
            {
                var item = new TextBlock(new Run(account.Email)) { Tag = account };
                this.AccountComboBox.Items.Add(item);

                if (recentWorld.Profile == account.Id)
                    this.AccountComboBox.SelectedItem = item;
            }
        }

        private void NewConnectionWindow_Closed(object sender, EventArgs e)
        {
            this._handle.ReceiveClose -= this._handle_ReceiveClose;
        }

        void _handle_ReceiveClose()
        {
            Dispatch.Invoke(() =>
            {
                this.DialogResult = false;
            });
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var profile = (Profile)((TextBlock)this.ProfileComboBox.SelectedItem).Tag;
            var account = (Account)((TextBlock)this.AccountComboBox.SelectedItem).Tag;

            var worldId = this.WorldIdTextBox.Text;
            var pass = account.Password.DecryptString().ToInsecureString();

            this._handle.DoSendSetData(account.Type, account.Email, pass, worldId, new[]
            {
                SettingsManager.DefaultProfilePath,
                profile.Database
            });

            this._recentWorld.Account = account.Id;
            this._recentWorld.Profile = profile.Id;
            this._recentWorld.WorldId = worldId;

            this.DialogResult = true;
        }
    }
}
