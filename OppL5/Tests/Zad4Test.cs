using System.Collections;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Zad4;

namespace Tests
{
    [TestClass]
    public class Zad4Test
    {
        /* this is the Comparison<int> to be converted */
        #region Public methods

        [TestMethod]
        public void TestAdapter()
        {
            var a = new ArrayList { 1, 5, 3, 3, 2, 4, 3 };
            var sortedA = a.Cast<int>().OrderBy(i => i).ToList();
            a.Sort(new ComparisonAdapter<int>(IntComparer));
            CollectionAssert.AreEqual(sortedA, a);
        }

        #endregion
        #region Private methods

        private static int IntComparer(int x, int y)
        {
            return x.CompareTo(y);
        }

        #endregion
    }
}