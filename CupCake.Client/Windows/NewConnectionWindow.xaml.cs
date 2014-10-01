using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using CupCake.Client.Settings;
using CupCake.Protocol;

namespace CupCake.Client.Windows
{
    /// <summary>
    ///     Interaction logic for NewConnectionWindow.xaml
    /// </summary>
    public partial class NewConnectionWindow
    {
        private readonly ClientHandle _handle;
        private readonly bool _isDebug;
        private readonly RecentWorld _recentWorld;

        public NewConnectionWindow(ClientHandle handle, RecentWorld recentWorld, bool isDebug)
        {
            this._recentWorld = recentWorld;
            this._isDebug = isDebug;
            this.InitializeComponent();

            this._handle = handle;
            this._handle.ReceiveClose += this._handle_ReceiveClose;

            this.Closed += this.NewConnectionWindow_Closed;

            foreach (Profile profile in SettingsManager.Settings.Profiles.OrderBy(v => v.Id))
            {
                var item = new TextBlock(new Run(profile.Name.GetVisualName())) {Tag = profile};
                this.ProfileComboBox.Items.Add(item);

                if (recentWorld.Profile == profile.Id)
                    this.ProfileComboBox.SelectedItem = item;
            }

            foreach (Account account in SettingsManager.Settings.Accounts.OrderBy(v => v.Id))
            {
                var item = new TextBlock(new Run((account.Name ?? account.Email).GetVisualName())) {Tag = account};
                this.AccountComboBox.Items.Add(item);

                if (recentWorld.Account == account.Id)
                    this.AccountComboBox.SelectedItem = item;
            }

            this.WorldIdTextBox.Text = recentWorld.WorldId;
        }

        private void NewConnectionWindow_Closed(object sender, EventArgs e)
        {
            this._handle.ReceiveClose -= this._handle_ReceiveClose;
        }

        private void _handle_ReceiveClose()
        {
            Dispatch.Invoke(() =>
            {
                if (this.DialogResult == null)
                    this.DialogResult = false;
            });
        }

        private void ConnectButton_Click(object sender, RoutedEventArgs e)
        {
            int pId;
            string pFolder;
            DatabaseType dbType;
            string dbCs;

            if (this.ProfileComboBox.SelectedItem != null)
            {
                var profile = (Profile)((TextBlock)this.ProfileComboBox.SelectedItem).Tag;
                pId = profile.Id;
                pFolder = profile.Folder;

                Database database = SettingsManager.Settings.Databases.FirstOrDefault(db => db.Id == profile.Database);

                if (database != null)
                {
                    dbType = database.Type;
                    dbCs = database.ConnectionString;
                }
                else
                {
                    MessageBoxHelper.Show(this, "Unable to load database data",
                        "The profile's database was not found, make sure it still exists.");
                    return;
                }
            }
            else
            {
                MessageBoxHelper.Show(this, "Profile not set",
                    "Please select a profile. If none are available, use the Settings -> Manage Profiles window to create a new one.");
                return;
            }

            int aId = default(int);
            string aEmail = String.Empty;
            string aPass = String.Empty;

            if (this.AccountComboBox.SelectedItem != null)
            {
                var account = (Account)((TextBlock)this.AccountComboBox.SelectedItem).Tag;
                aId = account.Id;
                aEmail = account.Email;
                aPass = account.Password.DecryptString().ToInsecureString();
            }

            string worldId = this.WorldIdTextBox.Text;

            var folders = new List<string>
            {
                SettingsManager.PluginsPath,
                pFolder
            };

            if (this._isDebug)
                folders.Add(SettingsManager.DebugPath);

            string settings = Path.Combine(pFolder, "ServerSettings.xml");
            if (!File.Exists(settings))
                settings = null;

            this._handle.DoSendSetData(aEmail, aPass, worldId, folders.ToArray(), dbType, dbCs, settings);

            this._recentWorld.Account = aId;
            this._recentWorld.Profile = pId;
            this._recentWorld.WorldId = worldId;

            this.DialogResult = true;
        }

        private void EditProfileButton_Click(object sender, RoutedEventArgs e)
        {
            new EditListWindow(EditListType.Profile) {Owner = this}.ShowDialog();
        }

        private void EditAccountButton_Click(object sender, RoutedEventArgs e)
        {
            new EditListWindow(EditListType.Account) {Owner = this}.ShowDialog();
        }
    }
}