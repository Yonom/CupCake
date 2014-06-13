﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CupCake.Core.Events;

namespace CupCake.World
{
    public class ResizeWorldEvent : Event
    {
        public int NewHeight { get; set; }
        public int NewWidth { get; set; }

        public ResizeWorldEvent(int newWidth,int newHeight)
        {
            this.NewHeight = newHeight;
            this.NewWidth = newWidth;
        }
    }
}
