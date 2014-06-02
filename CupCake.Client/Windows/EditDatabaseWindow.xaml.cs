using System.Windows;
using CupCake.Client.Settings;
using CupCake.Protocol;

namespace CupCake.Client.Windows
{
    /// <summary>
    ///     Interaction logic for EditDatabaseWindow.xaml
    /// </summary>
    public partial class EditDatabaseWindow
    {
        private readonly Database _database;

        public EditDatabaseWindow(Database database, bool isNew)
        {
            this._database = database;
            this.InitializeComponent();

            this.Title = isNew
                ? "New Database"
                : "Edit Database";

            this.NameTextBox.Text = database.Name;
            this.EngineComboBox.SelectedIndex = (int)database.Type;
            this.CsTextBox.Text = database.ConnectionString;
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = false;
        }

        private void OkButton_Click(object sender, RoutedEventArgs e)
        {
            this._database.Name = this.NameTextBox.Text;
            this._database.Type = (DatabaseType)this.EngineComboBox.SelectedIndex;
            this._database.ConnectionString = this.CsTextBox.Text;

            this.DialogResult = true;
        }
    }
}