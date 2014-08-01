using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using CupCake.Core;

namespace CupCake.Tests.Core
{
    [TestClass]
    public class RectangleTests
    {
        [TestMethod]
        public void EqualsTest()
        {
            Rectangle a = new Rectangle(0, 0, 0, 0);
            Rectangle b = new Rectangle(0, 0, 0, 0);
            Rectangle c = new Rectangle(0, 0, 1, 1);

            Assert.AreEqual(a, b);
            Assert.AreNotEqual(a, c);
        }
    }
}
