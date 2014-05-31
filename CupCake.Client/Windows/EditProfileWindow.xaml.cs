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
                ? "New Player"
                : "Edit Player";

            this._profile = profile;
            this.NameTextBox.Text = profile.Name;
            this.FolderTextBox.Text = profile.Folder ?? SettingsManager.ProfilesPath + "\\";
        }

        private void OkButton_Click(object sender, RoutedEventArgs e)
        {
            _profile.Name = this.NameTextBox.Text;
            _profile.Folder = this.FolderTextBox.Text;

            var path = this.FolderTextBox.Text;
            if (!Directory.Exists(path))
                Directory.CreateDirectory(path);

            this.DialogResult = true;
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
                DefaultDirectory = SettingsManager.CupCakePath,
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
