using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using PineAppleServer.Windows;

namespace PineAppleServer
{
    public static class MessageBoxHelper
    {
        public static void Show(Window owner, string title, string body)
        {
            new MessageBoxWindow(title, body) {Owner = owner}.ShowDialog();
        }
    }
}
