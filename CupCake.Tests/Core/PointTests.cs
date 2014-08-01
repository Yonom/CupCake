using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using CupCake.Core;

namespace CupCake.Tests.Core
{
    [TestClass]
    public class PointTests
    {
        [TestMethod]
        public void EqualsTest()
        {
            Point a = new Point(0, 0);
            Point b = new Point(0, 0);
            Point c = new Point(0, 1);

            Assert.AreEqual(a, b);
            Assert.AreNotEqual(a, c);
        }
    }
}
