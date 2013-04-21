using System;
using System.IO;
using System.Net.Mail;
using System.Net.Mime;
using System.Threading.Tasks;

namespace Zad1
{
    public class SmtpFacade
    {
        private const string ATTACHMENT_NAME = "attachment";

        #region Public methods

        public void Send(string @from, string to, string subject, string body, Stream stream = null,
                               string attachmentMimeType = null)
        {
            using (var client = new SmtpClient())
            {
                if (stream == null || attachmentMimeType == null)
                {
                    client.Send(@from, to, subject, body);
                }
                else
                {
                    if (string.IsNullOrEmpty(attachmentMimeType))
                    {
                        throw new ArgumentNullException();
                    }

                    //message
                    var mailMessage = new MailMessage(@from, to, subject, body);                                        

                    //attachment
                    var attachment = new Attachment(stream, new ContentType(attachmentMimeType));
                    var disposition = attachment.ContentDisposition;
                    // Suggest a file name for the attachment.
                    disposition.FileName = ATTACHMENT_NAME;
                    // Add the attachment to the message.
                    mailMessage.Attachments.Add(attachment);
                    client.Send(mailMessage);
                }
            }
        }

        #endregion
    }
}