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
            _database = database;
            InitializeComponent();

            Title = isNew
                ? "New Database"
                : "Edit Database";

            NameTextBox.Text = database.Name;
            EngineComboBox.SelectedIndex = (int) database.Type;
            CsTextBox.Text = database.ConnectionString;
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
        }

        private void OkButton_Click(object sender, RoutedEventArgs e)
        {
            _database.Name = NameTextBox.Text;
            _database.Type = (DatabaseType) EngineComboBox.SelectedIndex;
            _database.ConnectionString = CsTextBox.Text;

            DialogResult = true;
        }
    }
}