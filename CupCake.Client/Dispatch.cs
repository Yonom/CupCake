using System;
using System.Windows;

namespace CupCake.Client
{
    public static class Dispatch
    {
        public static void Invoke(Action callback)
        {
            Application.Current.Dispatcher.Invoke(callback);
        }

        public static void BeginInvoke(Action callback)
        {
            Application.Current.Dispatcher.BeginInvoke(callback);
        }
    }
}