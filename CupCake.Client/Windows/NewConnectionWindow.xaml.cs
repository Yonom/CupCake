using System;
using System.Collections.Generic;
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
            _recentWorld = recentWorld;
            _isDebug = isDebug;
            InitializeComponent();

            _handle = handle;
            _handle.ReceiveClose += _handle_ReceiveClose;

            Closed += NewConnectionWindow_Closed;

            foreach (Profile profile in SettingsManager.Settings.Profiles.OrderBy(v => v.Id))
            {
                var item = new TextBlock(new Run(profile.Name.GetVisualName())) {Tag = profile};
                ProfileComboBox.Items.Add(item);

                if (recentWorld.Profile == profile.Id)
                    ProfileComboBox.SelectedItem = item;
            }

            foreach (Account account in SettingsManager.Settings.Accounts.OrderBy(v => v.Id))
            {
                var item = new TextBlock(new Run((account.Name ?? account.Email).GetVisualName())) {Tag = account};
                AccountComboBox.Items.Add(item);

                if (recentWorld.Account == account.Id)
                    AccountComboBox.SelectedItem = item;
            }

            WorldIdTextBox.Text = recentWorld.WorldId;
        }

        private void NewConnectionWindow_Closed(object sender, EventArgs e)
        {
            _handle.ReceiveClose -= _handle_ReceiveClose;
        }

        private void _handle_ReceiveClose()
        {
            Dispatch.Invoke(() =>
            {
                if (DialogResult == null)
                    DialogResult = false;
            });
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            int pId;
            string pFolder;
            DatabaseType dbType;
            string dbCs;

            if (ProfileComboBox.SelectedItem != null)
            {
                var profile = (Profile) ((TextBlock) ProfileComboBox.SelectedItem).Tag;
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
            AccountType aType = default(AccountType);
            string aEmail = String.Empty;
            string aPass = String.Empty;

            if (AccountComboBox.SelectedItem != null)
            {
                var account = (Account) ((TextBlock) AccountComboBox.SelectedItem).Tag;
                aId = account.Id;
                aType = account.Type;
                aEmail = account.Email;
                aPass = account.Password.DecryptString().ToInsecureString();
            }

            string worldId = WorldIdTextBox.Text;

            var folders = new List<string>
            {
                SettingsManager.PluginsPath,
                pFolder
            };

            if (_isDebug)
                folders.Add(SettingsManager.DebugPath);

            _handle.DoSendSetData(aType, aEmail, aPass, worldId, folders.ToArray(), dbType, dbCs);

            _recentWorld.Account = aId;
            _recentWorld.Profile = pId;
            _recentWorld.WorldId = worldId;

            DialogResult = true;
        }
    }
}