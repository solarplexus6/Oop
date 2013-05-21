using System.Net;

namespace Zad1
{
    public class FtpGetFileCommand : ICommand
    {
        private readonly WebClient _client;
        private readonly string _uri;
        private readonly string _fileName;

        public FtpGetFileCommand(WebClient client, string uri, string fileName)
        {
            _client = client;
            _uri = uri;
            _fileName = fileName;
        }

        public void Execute()
        {
            _client.DownloadFile(_uri, _fileName);
        }
    }
}