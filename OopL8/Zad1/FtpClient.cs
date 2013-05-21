using System;
using System.CodeDom.Compiler;
using System.IO;
using System.Net;

namespace Zad1
{
    public class FtpClient
    {
        #region Properties

        public string CurrentFileContent { get; private set; }

        #endregion
        #region Public methods

        public void DownloadFile(string url)
        {
            var request = (FtpWebRequest)WebRequest.Create(url);
            request.Method = WebRequestMethods.Ftp.DownloadFile;

            var response = (FtpWebResponse) request.GetResponse();

            var responseStream = response.GetResponseStream();
            if (responseStream != null)
            {
                using (var reader = new StreamReader(responseStream))
                {
                    CurrentFileContent = reader.ReadToEnd();
                }
            }            

            response.Close();
        }

        #endregion
    }
}