using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CupCake.Core
{
    public class Rectangle
    {
        public int X { get; set; }
        public int Y { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }

        public Rectangle(int x, int y, int width, int height)
        {
            this.X = x;
            this.Y = y;
            this.Width = width;
            this.Height = height;
        }

        public Rectangle()
        {
            
        }
    }
}
