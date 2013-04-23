using System;
using System.IO;
using System.Linq;
using System.Net.Mime;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Zad1;
using netDumbster.smtp;

namespace Tests
{
    [TestClass]
    public class Zad1Test
    {
        #region Constants

        private const string FROM = "wieslav.dev@gmail.com";
        private const string TO = "solarplexus6@gmail.com";

        #endregion
        #region Private fields

        private static SimpleSmtpServer _server;

        #endregion
        #region Public static methods

        [ClassInitialize]
        public static void InitTests(TestContext context)
        {
            const int PORT = 2773;
            _server = SimpleSmtpServer.Start(PORT);
        }

        #endregion
        #region Public methods

        [TestInitialize]
        public void Init()
        {
            _server.ClearReceivedEmail();
        }

        [TestMethod]
        public void TestSendWithAttachment()
        {
            const string SUBJECT = "smtp facade attachment test";
            const string BODY = "attachment test body";
            const string ATTACHMENT_CONTENT = "test att content";

            var smtpFacade = new SmtpFacade();

            using (var stream = new MemoryStream(Encoding.UTF8.GetBytes(ATTACHMENT_CONTENT)))
            {
                smtpFacade.Send(FROM, TO, SUBJECT, BODY, stream, MediaTypeNames.Text.Plain);
            }

            var received = _server.ReceivedEmail.Single();
            var receivedAttachment = Convert.FromBase64String(received.MessageParts[1].BodyData);
            Assert.AreEqual(ATTACHMENT_CONTENT, Encoding.UTF8.GetString(receivedAttachment));
        }

        [TestMethod]
        public void TestSendWithoutAttachment()
        {
            const string SUBJECT = "smtp facade test";
            const string BODY = "test body";

            var smtpFacade = new SmtpFacade();
            smtpFacade.Send(FROM, TO, SUBJECT, BODY);

            var received = _server.ReceivedEmail.Single();
            Assert.AreEqual(FROM, received.FromAddress.ToString());
            Assert.AreEqual(TO, received.ToAddresses.Single().ToString());
            Assert.AreEqual(BODY, received.MessageParts.Single().BodyData);
        }

        #endregion
    }
}