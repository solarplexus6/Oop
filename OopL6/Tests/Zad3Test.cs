using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Zad3;

namespace Tests
{
    [TestClass]
    public class Zad3Test
    {
        [TestMethod]
        public void TestHeightVisitor()
        {
            var tree = new Tree();
            tree.Add(10);
            tree.Add(1);
            tree.Add(20);
            tree.Add(23);
            tree.Add(21);

            var visitor = new HeightVisitor();
            tree.Accept(visitor);

            Assert.AreEqual(3, visitor.TreeHeight);
        }
    }
}
