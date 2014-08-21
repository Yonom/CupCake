using CupCake.Core;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CupCake.Tests.Core
{
    [TestClass]
    public class PointTests
    {
        [TestMethod]
        public void EqualsTest()
        {
            var a = new Point(0, 0);
            var b = new Point(0, 0);
            var c = new Point(0, 1);

            Assert.AreEqual(a, b);
            Assert.AreNotEqual(a, c);
        }
    }
}