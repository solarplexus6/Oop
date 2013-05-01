using System;
using System.CodeDom.Compiler;
using System.IO;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Zad1;

namespace Tests
{
    [TestClass]
    public class Zad1Test
    {
        const string TEST_LOG = "test log";


        [TestMethod]
        public void ConsoleLoggerTest()
        {
            var logger = LoggerFactory.Instance.GetLogger(LogType.Console);

            using (var sw = new StringWriter())
            {
                Console.SetOut(sw);
                logger.Log(TEST_LOG);
                Assert.AreEqual(TEST_LOG + Environment.NewLine, sw.ToString());
            }
        }

        [TestMethod]
        public void NoneLoggerTest()
        {            
            var logger = LoggerFactory.Instance.GetLogger(LogType.None);

            using (var sw = new StringWriter())
            {
                Console.SetOut(sw);
                logger.Log(TEST_LOG + Environment.NewLine);
                Assert.AreEqual(String.Empty, sw.ToString());
            }                
        }

        [TestMethod]
        public void FileLoggerTest()
        {
            var tfc = new TempFileCollection();
            var fileName = tfc.AddExtension("txt");
            var logger = LoggerFactory.Instance.GetLogger(LogType.File, fileName);
            logger.Log(TEST_LOG);            
        }
    }
}
