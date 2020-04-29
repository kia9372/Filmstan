using Common.Operation;
using MimeKit;
using System.Threading.Tasks;

namespace Common.Notification
{
    public abstract class Notification
    {
        public abstract Task<OperationResult<string>> Send(string from, string subject, string content, string userName, string password, MailboxAddress to, int port, string smtpServer);
        public abstract Task<OperationResult<string>> Send(string lineNumber, string userApiKey, string phoneNumber, string message, string secrectKey);
    }
}
