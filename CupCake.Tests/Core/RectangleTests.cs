using CupCake.Core;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CupCake.Tests.Core
{
    [TestClass]
    public class RectangleTests
    {
        [TestMethod]
        public void EqualsTest()
        {
            var a = new Rectangle(0, 0, 0, 0);
            var b = new Rectangle(0, 0, 0, 0);
            var c = new Rectangle(0, 0, 1, 1);

            Assert.AreEqual(a, b);
            Assert.AreNotEqual(a, c);
        }
    }
}