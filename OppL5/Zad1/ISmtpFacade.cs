using System.IO;

namespace Zad1
{
    public interface ISmtpFacade
    {
        #region Public methods

        void Send(string @from, string to, string subject, string body, Stream stream = null,
                  string attachmentMimeType = null);

        #endregion
    }
}