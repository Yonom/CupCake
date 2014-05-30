using System;
using System.Collections;
using System.Windows;
using System.Windows.Controls;
using CupCake.Client.Settings;

namespace CupCake.Client.Windows
{
    /// <summary>
    /// Interaction logic for Profiles.xaml
    /// </summary>
    public partial class EditListWindow
    {
        private readonly EditListType _type;
        private readonly IList _collection; 

        private bool HasSelection {
            set
            {
                this.EditButton.IsEnabled = value;
                this.RemoveButton.IsEnabled = value;
            }
        }

        public EditListWindow(EditListType type)
        {
            this._type = type;

            InitializeComponent();
            this.HasSelection = false;

            if (type == EditListType.Profile) 
                this._collection = SettingsManager.Settings.Profiles;
            else 
                this._collection = SettingsManager.Settings.Accounts;

            this.Title = type == EditListType.Profile
                ? "Manage Profiles"
                : "Manage Accounts";

            this.ItemsListBox.SelectionChanged += this.ItemsListBox_SelectionChanged;
            this.Closing += this.EditListWindow_Closing;

            foreach (IConfig profile in this._collection)
            {
                this.AddToList(profile);
            }
        }

        void EditListWindow_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            SettingsManager.Save();
        }

        void ItemsListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            this.HasSelection = this.ItemsListBox.SelectedItem != null;
        }

        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            IConfig p;
            if (this._type == EditListType.Profile) 
                p = new Profile();
            else 
                p = new Account();

            if (this.EditItem(p, true) == true)
            {
                this.AddItem(p);
            }
        }

        private void EditButton_Click(object sender, RoutedEventArgs e)
        {
            var uiP = (TextBlock)this.ItemsListBox.SelectedItem;
            var p = (IConfig)uiP.Tag;
            var cloneP = p.Clone();
            if (this.EditItem(cloneP, false) == true)
            {
                this.RemoveItem(uiP, p);
                this.AddItem(cloneP);
            }
        }

        private void RemoveButton_Click(object sender, RoutedEventArgs e)
        {
            var uiP = (TextBlock)this.ItemsListBox.SelectedItem;
            var p = (IConfig)uiP.Tag;
            this.RemoveItem(uiP, p);
        }

        private void AddItem(IConfig p)
        {
            this._collection.Add(p);
            this.AddToList(p);
        }

        private void AddToList(IConfig p)
        {
            var textBlockStyle = this.FindResource("TextBlockStyle") as Style;
            this.ItemsListBox.Items.Add(new TextBlock
            {
                Style = textBlockStyle,
                Text = p.Name,
                Tag = p
            });
        }

        private void RemoveItem(TextBlock uiP, IConfig p)
        {
            this._collection.Remove(p);
            this.ItemsListBox.Items.Remove(uiP);
        }

        private bool? EditItem(IConfig item, bool isNew)
        {
            if (this._type == EditListType.Profile)
            {
                var profile = (Profile)item;
                var editProfile = new EditProfileWindow(profile, isNew) { Owner = this };
                return editProfile.ShowDialog();
            } 

            if (this._type == EditListType.Account)
            {
                var account = (Account)item;
                var editAccount = new EditAccountWindow(account, isNew) { Owner = this };
                return editAccount.ShowDialog();
            }
            return null;
        }
    }
}
