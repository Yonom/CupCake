using System.Windows;
using CupCake.Client.Windows;

namespace CupCake.Client
{
    public static class MessageBoxHelper
    {
        public static bool? Show(Window owner, string title, string body)
        {
            return new MessageBoxWindow(title, body) {Owner = owner}.ShowDialog();
        }
    }
}