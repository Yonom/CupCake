using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CupCake.Client
{
    public static class StringExtensions
    {
        public static string GetVisualName(this string str)
        {
            return String.IsNullOrWhiteSpace(str) ? "<Unnamed>" : str;
        }
    }
}
