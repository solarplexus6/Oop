using Microsoft.VisualStudio.TestTools.UnitTesting;
using Zad2;

namespace Tests
{
    [TestClass]
    public class Zad2Tests
    {
        #region Public methods

        [TestMethod]
        public void TestDb()
        {
            var handler = new DbDataAccessHandler();
            handler.Execute();
            Assert.AreEqual(handler.Result, 60);
        }

        #endregion
    }
}