using Microsoft.VisualStudio.TestTools.UnitTesting;
using Zad3;

namespace Tests
{
    [TestClass]
    public class Zad3Tests
    {
        #region Public methods

        [TestMethod]
        public void TestDb()
        {
            var du = new DataUser(new DbDataAccessStrategy());
            var result = du.UseData();

            Assert.AreEqual(result, 60);
        }

        [TestMethod]
        public void TestXml()
        {
            var du = new DataUser(new XmlDataAccessStrategy("cd_catalog.xml"));
            var result = du.UseData();

            Assert.AreEqual(result, 7);
        }

        #endregion 
    }
}