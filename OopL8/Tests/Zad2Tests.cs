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
            DataAccessHandler handler = new DbDataAccessHandler();
            handler.Execute();
            Assert.AreEqual(((DbDataAccessHandler)handler).Result, 60);
        }

        [TestMethod]
        public void TestXml()
        {
            DataAccessHandler handler = new XmlDataAccessHandler("cd_catalog.xml");
            handler.Execute();
            Assert.AreEqual(((XmlDataAccessHandler)handler).Result, "CATALOG");
        }

        #endregion
    }
}