using System.Deployment.Application;
using System.Reflection;

namespace CupCake.Client.Windows
{
    /// <summary>
    ///     Interaction logic for AboutWindow.xaml
    /// </summary>
    public partial class AboutWindow
    {
        public AboutWindow()
        {
            this.InitializeComponent();

            this.VersionRun.Text = ApplicationDeployment.IsNetworkDeployed
                ? ApplicationDeployment.CurrentDeployment.CurrentVersion.ToString()
                : "Debug";
        }
    }
}