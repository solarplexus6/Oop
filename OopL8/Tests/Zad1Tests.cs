using System.CodeDom.Compiler;
using System.IO;
using System.Net;
using System.Threading;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Zad1;

namespace Tests
{
    [TestClass]
    public class Zad1Tests
    {
        #region Constants

        private const string FTP_URI = "ftp://ftp.freebsd.org/pub/FreeBSD/README.TXT";
        private const string HTTP_URI = "https://raw.github.com/solarplexus6/po/master/README.md";

        #endregion
        #region Public methods

        [TestMethod]
        public void TestCopy()
        {
            var tfc = new TempFileCollection();
            var srcFileName = tfc.AddExtension("tmp1");
            var destFileName = tfc.AddExtension("tmp2");

            var fileManager = new FileManager();
            var createCommand = new CreateRandomFileCommand(fileManager, srcFileName);
            var copyCommand = new CopyFileCommand(fileManager, srcFileName, destFileName);

            var invoker = new Invoker();

            invoker.Execute(createCommand);
            invoker.Execute(copyCommand);

            using (StreamReader srcReader = File.OpenText(srcFileName),
                                destReader = File.OpenText(destFileName))
            {
                var srcContent = srcReader.ReadToEnd();
                var destContent = destReader.ReadToEnd();
                Assert.AreEqual(srcContent, destContent);
            }
        }

        [TestMethod]
        public void TestFtp()
        {
            const string CONTENT_SUBSTRING = "FreeBSD";

            // receiver
            var client = new WebClient();
            var tfc = new TempFileCollection();
            var fileName = tfc.AddExtension("txt");
            var command = new FtpGetFileCommand(client, FTP_URI, fileName);

            var invoker = new Invoker();

            invoker.Execute(command);
            using (var reader = File.OpenText(fileName))
            {
                var content = reader.ReadToEnd();
                Assert.IsTrue(content.Contains(CONTENT_SUBSTRING));
            }
        }

        [TestMethod]
        public void TestHttp()
        {
            const string CONTENT_SUBSTRING = "POPrac4FFT";

            var client = new WebClient();
            var tfc = new TempFileCollection();
            var fileName = tfc.AddExtension("txt");

            var command = new HttpGetFileCommand(client, HTTP_URI, fileName);

            var invoker = new Invoker();

            invoker.Execute(command);

            using (var reader = File.OpenText(fileName))
            {
                var content = reader.ReadToEnd();
                Assert.IsTrue(content.Contains(CONTENT_SUBSTRING));
            }
        }

        [TestMethod]
        public void TestInvokerThreads()
        {
            var tfc = new TempFileCollection();
            var ftpFileName = tfc.AddExtension("tmp1");
            var httpFileName = tfc.AddExtension("tmp2");
            var fileName = tfc.AddExtension("txt");

            var fileManager = new FileManager();
            var randFileCommand = new CreateRandomFileCommand(fileManager, fileName);

            var ftpClient = new WebClient();
            var httpClient = new WebClient();
            var ftpCommand = new FtpGetFileCommand(ftpClient, FTP_URI, ftpFileName);
            var httpCommand = new HttpGetFileCommand(httpClient, HTTP_URI, httpFileName);

            var invoker = new Invoker(true);

            invoker.Execute(ftpCommand);
            invoker.Execute(randFileCommand);            
            invoker.Execute(httpCommand);

            Thread.Sleep(1000);

            using (var reader = File.OpenText(ftpFileName))
            {
                var content = reader.ReadToEnd();
                Assert.IsFalse(string.IsNullOrEmpty(content));
            }
            using (var reader = File.OpenText(httpFileName))
            {
                var content = reader.ReadToEnd();
                Assert.IsFalse(string.IsNullOrEmpty(content));
            }
            using (var reader = File.OpenText(fileName))
            {
                var content = reader.ReadToEnd();
                Assert.IsFalse(string.IsNullOrEmpty(content));
            }
        }

        [TestMethod]
        public void TestRandomFile()
        {
            var tfc = new TempFileCollection();
            var fileName = tfc.AddExtension("txt");

            var fileManager = new FileManager();
            var command = new CreateRandomFileCommand(fileManager, fileName);

            var invoker = new Invoker();

            invoker.Execute(command);

            using (var reader = File.OpenText(fileName))
            {
                var content = reader.ReadToEnd();
                Assert.IsFalse(string.IsNullOrEmpty(content));
            }
        }

        #endregion
    }
}