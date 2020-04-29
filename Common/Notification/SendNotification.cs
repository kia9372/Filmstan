using Common.Operation;
using MailKit.Net.Smtp;
using MimeKit;
using SmsIrRestfulNetCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Common.Notification
{
    public class SendNotification : Notification
    {
        public SendNotification()
        {
        }
        /// <summary>
        /// Send Email Notifiaction
        /// </summary>
        /// <param name="from"></param>
        /// <param name="subject"></param>
        /// <param name="content"></param>
        /// <param name="userName"></param>
        /// <param name="password"></param>
        /// <param name="to"></param>
        /// <param name="port"></param>
        /// <param name="smtpServer"></param>
        /// <returns></returns>
        public override async Task<OperationResult<string>> Send(string from, string subject, string content, string userName, string password, MailboxAddress to, int port, string smtpServer)
        {
            using (var client = new SmtpClient())
            {
                var emailMessage = new MimeMessage();
                try
                {
                    client.Connect(smtpServer, port, true);
                    client.AuthenticationMechanisms.Remove("XOAUTH2");
                    client.Authenticate(userName, password);
                    emailMessage.From.Add(new MailboxAddress(from));
                    emailMessage.To.Add(to);
                    emailMessage.Subject = subject;
                    emailMessage.Body = new TextPart(MimeKit.Text.TextFormat.Text) { Text = content };
                }
                catch (Exception ex)
                {
                    return OperationResult<string>.BuildFailure(ex);
                }
                finally
                {
                    await client.DisconnectAsync(true);
                    client.Dispose();
                }
            }
            return OperationResult<string>.BuildSuccessResult("Success Send Email");
        }


        /// <summary>
        /// Send Sms Function
        /// </summary>
        /// <param name="lineNumber"></param>
        /// <param name="userApiKey"></param>
        /// <param name="phoneNumber"></param>
        /// <param name="message"></param>
        /// <param name="secrectKey"></param>
        /// <returns></returns>
        public override async Task<OperationResult<string>> Send(string lineNumber, string userApiKey, string phoneNumber, string message, string secrectKey)
        {
            var token = new Token().GetToken(userApiKey, secrectKey);

            var restVerificationCode = new RestVerificationCode()
            {
                Code = message,
                MobileNumber = phoneNumber
            };

            var restVerificationCodeRespone = new VerificationCode().Send(token, restVerificationCode);
            if (restVerificationCodeRespone.IsSuccessful)
            {
                return OperationResult<string>.BuildSuccessResult(restVerificationCodeRespone.Message);
            }
            return OperationResult<string>.BuildFailure(restVerificationCodeRespone.Message);
        }
    }
}
