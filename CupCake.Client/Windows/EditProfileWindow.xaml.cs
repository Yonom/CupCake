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
            InitializeComponent();

            Title = isNew
                ? "New Profile"
                : "Edit Profile";

            foreach (Database database in SettingsManager.Settings.Databases.OrderBy(v => v.Id))
            {
                var item = new TextBlock(new Run(database.Name.GetVisualName())) {Tag = database};
                DatabaseComboBox.Items.Add(item);

                if (profile.Database == database.Id)
                    DatabaseComboBox.SelectedItem = item;
            }

            _profile = profile;
            NameTextBox.Text = profile.Name;
            FolderTextBox.Text = profile.Folder ?? SettingsManager.ProfilesPath + "\\";
        }

        private void OkButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                string path = FolderTextBox.Text;
                if (!Directory.Exists(path))
                    Directory.CreateDirectory(path);


                _profile.Name = NameTextBox.Text;
                _profile.Folder = FolderTextBox.Text;

                if (DatabaseComboBox.SelectedItem != null)
                {
                    var database = (Database) ((TextBlock) DatabaseComboBox.SelectedItem).Tag;
                    _profile.Database = database.Id;
                }
                else
                {
                    MessageBoxHelper.Show(this, "Database not set",
                        "Please select a database. If none are available, use the Settings -> Manage Databases window to create a new one.");
                    return;
                }

                DialogResult = true;
            }
            catch (IOException ex)
            {
                MessageBoxHelper.Show(this, "Error", "Unable to create folder for profile: " + ex.Message);
            }
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
        }

        private void NameTextBox_OnTextChanged(object sender, TextChangedEventArgs e)
        {
            string folderName = FolderTextBox.Text;

            if (folderName.EndsWith("\\"))
                FolderTextBox.Text += NameTextBox.Text;
            else if (!String.IsNullOrEmpty(_lastName) && folderName.EndsWith(_lastName))
                FolderTextBox.Text = folderName.Substring(0, folderName.Length - _lastName.Length) +
                                     NameTextBox.Text;


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
                string folder = dlg.FileName;

                FolderTextBox.Text = folder;
            }
        }
    }
}