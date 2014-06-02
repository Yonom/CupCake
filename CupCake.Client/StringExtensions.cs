using System;
using CupCake.Client.Settings;

namespace CupCake.Client
{
    public static class StringExtensions
    {
        public static string GetVisualName(this string str)
        {
            return String.IsNullOrWhiteSpace(str) ? SettingsManager.UnnamedString : str;
        }
    }
}