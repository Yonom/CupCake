using System.Threading;
using System.Windows;
using CupCake.Client.Windows;

namespace CupCake.Client
{
    /// <summary>
    ///     Interaction logic for App.xaml
    /// </summary>
    public partial class App
    {
        private void Application_Startup(object sender, StartupEventArgs e)
        {
            bool aIsNewInstance;
            using (
                new Mutex(true, "CupCake Single Instance Mutex: {a823d281-0a70-4bbe-b486-d4b7984e2312}",
                    out aIsNewInstance))
            {
                if (!aIsNewInstance)
                {
                    new MessageBoxWindow("There can only be one CupCake!",
                        "There is already an instance of cupcake running running!").ShowDialog();
                }
                else
                {
                    new MainWindow().ShowDialog();
                }
            }

            this.Shutdown();
        }
    }
}