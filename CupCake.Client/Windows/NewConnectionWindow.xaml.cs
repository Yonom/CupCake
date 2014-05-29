using System;
using System.Windows;
using CupCake.Protocol;

namespace CupCake.Client.Windows
{
    /// <summary>
    /// Interaction logic for NewConnectionWindow.xaml
    /// </summary>
    public partial class NewConnectionWindow
    {
        private readonly ClientHandle _handle;

        public NewConnectionWindow(ClientHandle handle)
        {
            InitializeComponent();

            this._handle = handle;
            this._handle.ReceiveClose += this._handle_ReceiveClose;

            this.Closed += this.NewConnectionWindow_Closed;
        }

        private void NewConnectionWindow_Closed(object sender, EventArgs e)
        {
            this._handle.ReceiveClose -= this._handle_ReceiveClose;
        }

        void _handle_ReceiveClose()
        {
            Application.Current.Dispatcher.BeginInvoke(new Action(() =>
            {
                this.DialogResult = false;
            }));
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            this._handle.DoSendSetData("sepi1376@gmail.com", "1346279", "PWWkBWyGyla0I", null);
            this.DialogResult = true;
        }
    }
}
