using Common.Operation;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Common.Notification
{
    public class NotificationSms : SendNotification
    {
        private readonly Notification notification;

        public NotificationSms(Notification notification) 
        {
            this.notification = notification;
        }

        public override Task<OperationResult<string>> Send(string lineNumber, string userApiKey, string phoneNumber, string message, string secrectKey)
        {
            return base.Send(lineNumber, userApiKey, phoneNumber, message, secrectKey);
        }
    }
}
