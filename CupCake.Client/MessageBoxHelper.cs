using System.Windows;
using CupCake.Client.Windows;

namespace CupCake.Client
{
    public static class MessageBoxHelper
    {
        public static void Show(Window owner, string title, string body)
        {
            new MessageBoxWindow(title, body) {Owner = owner}.ShowDialog();
        }
    }
}
