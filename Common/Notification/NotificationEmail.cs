using Common.Operation;
using MimeKit;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Common.Notification
{
    public class NotificationEmail : SendNotification
    {
        private readonly Notification notification;

        public NotificationEmail(Notification notification) 
        {
            this.notification = notification;
        }

        public override Task<OperationResult<string>> Send(string from, string subject, string content, string userName, string password, MailboxAddress to, int port, string smtpServer)
        {
            return base.Send(from, subject, content, userName, password, to, port, smtpServer);
        }
    }
}
