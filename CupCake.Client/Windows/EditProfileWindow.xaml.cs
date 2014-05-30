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

        public EditProfileWindow(Profile profile, bool isNew)
        {
            InitializeComponent();

            this.Title = isNew
                ? "New Player"
                : "Edit Player";

            this.Closing += EditProfileWindow_Closing;

            this._profile = profile;
            this.NameTextBox.Text = profile.Name;
            this.FolderTextBox.Text = profile.Folder ?? SettingsManager.ProfilesPath + "\\";
        }

        void EditProfileWindow_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            var path = this.FolderTextBox.Text;
            if (!Directory.Exists(path))
                Directory.CreateDirectory(path);
        }

        private void OkButton_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = true;
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = false;
        }

        private void FolderTextBox_OnTextChanged(object sender, TextChangedEventArgs e)
        {
            _profile.Folder = FolderTextBox.Text;
        }

        private void NameTextBox_OnTextChanged(object sender, TextChangedEventArgs e)
        {
            var folderName = this.FolderTextBox.Text;

            if (folderName.EndsWith("\\"))
                this.FolderTextBox.Text += this.NameTextBox.Text;
            else if (!String.IsNullOrEmpty(_profile.Name) &&  folderName.EndsWith(_profile.Name))
                this.FolderTextBox.Text = folderName.Substring(0, folderName.Length - _profile.Name.Length) + this.NameTextBox.Text;


            _profile.Name = NameTextBox.Text;
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
