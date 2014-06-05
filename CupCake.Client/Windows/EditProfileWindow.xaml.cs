using System;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using CupCake.Client.Settings;
using Microsoft.WindowsAPICodePack.Dialogs;

namespace CupCake.Client.Windows
{
    /// <summary>
    ///     Interaction logic for EditProfileWindow.xaml
    /// </summary>
    public partial class EditProfileWindow
    {
        private readonly Profile _profile;
        private string _lastName;

        public EditProfileWindow(Profile profile, bool isNew)
        {
            this.InitializeComponent();

            this.Title = isNew
                ? "New Profile"
                : "Edit Profile";

            foreach (Database database in SettingsManager.Settings.Databases.OrderBy(v => v.Id))
            {
                var item = new TextBlock(new Run(database.Name.GetVisualName())) {Tag = database};
                this.DatabaseComboBox.Items.Add(item);

                if (profile.Database == database.Id)
                    this.DatabaseComboBox.SelectedItem = item;
            }

            this._profile = profile;
            this.NameTextBox.Text = profile.Name;
            this.FolderTextBox.Text = profile.Folder ?? SettingsManager.ProfilesPath + "\\";
        }

        private void OkButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                string path = this.FolderTextBox.Text;
                if (!Directory.Exists(path))
                    Directory.CreateDirectory(path);


                this._profile.Name = this.NameTextBox.Text;
                this._profile.Folder = this.FolderTextBox.Text;

                if (this.DatabaseComboBox.SelectedItem != null)
                {
                    var database = (Database)((TextBlock)this.DatabaseComboBox.SelectedItem).Tag;
                    this._profile.Database = database.Id;
                }
                else
                {
                    MessageBoxHelper.Show(this, "Database not set",
                        "Please select a database. If none are available, use the Settings -> Manage Databases window to create a new one.");
                    return;
                }

                this.DialogResult = true;
            }
            catch (IOException ex)
            {
                MessageBoxHelper.Show(this, "Error", "Unable to create folder for profile: " + ex.Message);
            }
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = false;
        }

        private void NameTextBox_OnTextChanged(object sender, TextChangedEventArgs e)
        {
            string folderName = this.FolderTextBox.Text;

            if (folderName.EndsWith("\\"))
                this.FolderTextBox.Text += this.NameTextBox.Text;
            else if (!String.IsNullOrEmpty(this._lastName) && folderName.EndsWith(this._lastName))
                this.FolderTextBox.Text = folderName.Substring(0, folderName.Length - this._lastName.Length) +
                                          this.NameTextBox.Text;


            this._lastName = this.NameTextBox.Text;
        }

        private void BrowseButton_Click(object sender, RoutedEventArgs e)
        {
            var dlg = new CommonOpenFileDialog
            {
                Title = "Choose profile folder",
                IsFolderPicker = true,
                AddToMostRecentlyUsedList = false,
                AllowNonFileSystemItems = false,
                DefaultDirectory = SettingsManager.ProfilesPath,
                EnsurePathExists = false,
                EnsureReadOnly = false,
                EnsureValidNames = true,
                Multiselect = false,
                ShowPlacesList = true
            };

            if (dlg.ShowDialog() == CommonFileDialogResult.Ok)
            {
                string folder = dlg.FileName;

                this.FolderTextBox.Text = folder;
            }
        }
    }
}