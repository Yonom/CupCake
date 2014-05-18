using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CupCake.EE
{
    public class Point
    {
        public int X { get; private set; }
        public int Y { get; private set; }

        public Point(int x, int y)
        {
            this.X = x;
            this.Y = y;
        }
    }
}
