using System;
using Microsoft.QualityTools.Testing.Fakes;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Zad3;
using netDumbster.smtp;

namespace Tests
{
    [TestClass]
    public class Zad3Test
    {
        private static SimpleSmtpServer _server;
        private const string FROM = "wieslav.dev@gmail.com";
        private const string TO = "solarplexus6@gmail.com";
        const string SUBJECT = "smtp facade test";
        const string BODY = "test body";

        [ClassInitialize]
        public static void InitTests(TestContext context)
        {
            const int PORT = 2773;
            _server = SimpleSmtpServer.Start(PORT);
        }


        [TestInitialize]
        public void Init()
        {
            _server.ClearReceivedEmail();
        }

        #region Public methods

        [TestMethod]
        [ExpectedException(typeof (InvalidOperationException))]
        public void TestProxyDisabled()
        {
            const int SHIM_HOUR = 3;
            var now = DateTime.Now;

            using (ShimsContext.Create())
            {
                System.Fakes.ShimDateTime.NowGet =
                    () => new DateTime(now.Year, now.Month, now.Day, SHIM_HOUR,
                                       now.Minute, now.Second);
                var proxy = new SmtpFacadeProxy();
                proxy.Send(FROM, TO, SUBJECT, BODY);                
            }
        }

        [TestMethod]
        public void TestProxyEnabled()
        {
            const int SHIM_HOUR = 10;
            var now = DateTime.Now;

            using (ShimsContext.Create())
            {
                System.Fakes.ShimDateTime.NowGet =
                    () => new DateTime(now.Year, now.Month, now.Day, SHIM_HOUR,
                                       now.Minute, now.Second);
                var proxy = new SmtpFacadeProxy();                
                proxy.Send(FROM, TO, SUBJECT, BODY);
                Assert.AreEqual(1, _server.ReceivedEmailCount);
            }
        }

        #endregion
    }
}