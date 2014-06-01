using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using CupCake.Client.Settings;
using CupCake.Protocol;
using Microsoft.WindowsAPICodePack.Dialogs;

namespace CupCake.Client.Windows
{
    /// <summary>
    /// Interaction logic for EditProfileWindow.xaml
    /// </summary>
    public partial class EditProfileWindow
    {
        private readonly Profile _profile;
        private string _lastName;

        public EditProfileWindow(Profile profile, bool isNew)
        {
            InitializeComponent();

            this.Title = isNew
                ? "New Profile"
                : "Edit Profile";

            foreach (var database in SettingsManager.Settings.Databases.OrderBy(v => v.Id))
            {
                var item = new TextBlock(new Run(database.Name.GetVisualName())) { Tag = database };
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
                var path = this.FolderTextBox.Text;
                if (!Directory.Exists(path))
                    Directory.CreateDirectory(path);


                _profile.Name = this.NameTextBox.Text;
                _profile.Folder = this.FolderTextBox.Text;

                if (this.DatabaseComboBox.SelectedItem != null)
                {
                    var database = (Database)((TextBlock)this.DatabaseComboBox.SelectedItem).Tag;
                    _profile.Database = database.Id;
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
            var folderName = this.FolderTextBox.Text;

            if (folderName.EndsWith("\\"))
                this.FolderTextBox.Text += this.NameTextBox.Text;
            else if (!String.IsNullOrEmpty(_lastName) && folderName.EndsWith(_lastName))
                this.FolderTextBox.Text = folderName.Substring(0, folderName.Length - _lastName.Length) + this.NameTextBox.Text;


            _lastName = NameTextBox.Text;
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
                var folder = dlg.FileName;

                this.FolderTextBox.Text = folder;
            }
        }
    }
}
