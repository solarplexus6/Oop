using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Zad2;

namespace Testy
{
    [TestClass]
    public class Zad2Test
    {
        #region Constants

        private const int TEST_DAY = 23;
        private const int TEST_MONTH = 10;

        private const string TEST_STRING = "zxcf asd xcv";
        private const int TEST_YEAR = 996;

        #endregion
        #region Public methods

        [TestMethod]
        [ExpectedException(typeof (MissingMethodException))]
        public void TestCreateObjectParametersException()
        {
            var factory = new GenericFactory();
            factory.CreateObject("System.String");
        }

        public void TestFactoryProductEquals()
        {
            var factory = new GenericFactory();

            var str = factory.CreateObject("System.String", parameters: TEST_STRING.ToCharArray());
            Assert.AreEqual(str, TEST_STRING);


            var dt1 = factory.CreateObject("System.DateTime", false, TEST_YEAR, TEST_MONTH, TEST_DAY);
            var dt2 = new DateTime(TEST_YEAR, TEST_MONTH, TEST_DAY);
            Assert.AreEqual(dt1, dt2);
        }

        [TestMethod]
        public void TestFactoryProductProperType()
        {
            var factory = new GenericFactory();

            var str = factory.CreateObject("System.String", parameters: new[] {'z', 'c', 'v'});
            Assert.IsInstanceOfType(str, typeof (string));

            var dt = factory.CreateObject("System.DateTime");
            Assert.IsInstanceOfType(dt, typeof (DateTime));

            var genericFactory = factory.CreateObject("Zad2.GenericFactory");
            Assert.IsInstanceOfType(genericFactory, typeof (GenericFactory));
        }

        [TestMethod]
        public void TestFactorySingletonFalseFlag()
        {
            var factory = new GenericFactory();
            var f1 = factory.CreateObject("Zad2.GenericFactory");
            var f2 = factory.CreateObject("Zad2.GenericFactory");
            Assert.AreNotSame(f1, f2);

            var str1 = factory.CreateObject("System.String", parameters: TEST_STRING.ToCharArray());
            var str2 = factory.CreateObject("System.String", parameters: TEST_STRING.ToCharArray());
            Assert.AreNotSame(str1, str2);

            var dt1 = factory.CreateObject("System.DateTime", false, TEST_YEAR, TEST_MONTH, TEST_DAY);
            var dt2 = factory.CreateObject("System.DateTime", false, TEST_YEAR, TEST_MONTH, TEST_DAY);
            Assert.AreNotSame(dt1, dt2);
        }

        [TestMethod]
        public void TestFactorySingletonFlag()
        {
            var factory = new GenericFactory();

            var f1 = factory.CreateObject("Zad2.GenericFactory", true);
            var f2 = factory.CreateObject("Zad2.GenericFactory", true);
            Assert.AreSame(f1, f2);

            var str1 = factory.CreateObject("System.String", true, TEST_STRING.ToCharArray());
            var str2 = factory.CreateObject("System.String", true, TEST_STRING.ToCharArray());
            Assert.AreSame(str1, str2);

            var dt1 = factory.CreateObject("System.DateTime", true, TEST_YEAR, TEST_MONTH, TEST_DAY);
            var dt2 = factory.CreateObject("System.DateTime", true, TEST_YEAR, TEST_MONTH, TEST_DAY);
            Assert.AreSame(dt1, dt2);
        }

        #endregion
    }
}