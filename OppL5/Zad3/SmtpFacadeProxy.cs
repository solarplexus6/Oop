using System;
using System.IO;
using Zad1;

namespace Zad3
{
    public class SmtpFacadeProxy : ISmtpFacade
    {
        #region Constants

        private const int MAX_HOUR = 22;
        private const int MIN_HOUR = 8;

        #endregion
        #region Private fields

        private readonly SmtpFacade _smtpFacade = new SmtpFacade();

        #endregion
        #region ISmtpFacade Members

        public void Send(string @from, string to, string subject, string body, Stream stream = null,
                         string attachmentMimeType = null)
        {
            var nowHour = DateTime.Now.Hour;
            if (nowHour < MIN_HOUR || nowHour >= MAX_HOUR)
            {
                const string OBJECT_DISABLED = "Object is disabled at this time";
                throw new InvalidOperationException(OBJECT_DISABLED);
            }

            _smtpFacade.Send(@from, to, subject, body, stream, attachmentMimeType);
        }

        #endregion
    }
}