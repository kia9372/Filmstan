using Common.FilmStanEnums;
using Common.FilmstanExtentions;
using Common.Notification;
using Common.Operation;
using Common.SiteEnums;
using DataTransfer.EmailSettingDtos;
using MimeKit;
using SiteService.Repositories.Implementation;
using SiteService.SendNotificationStrategy;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Framework.SendNotificationStrategy
{
    public class SendNotifByEmail : SendNotif
    {
        private readonly IDomainUnitOfWork unitOfWork;

        public SendNotifByEmail(IDomainUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }
        public override SendCodeVerification SendCodeType => SendCodeVerification.Email;

        public async override Task<OperationResult<string>> SendCodeAsync(string content, string to, CancellationToken cancellationToken)
        {
            SendNotification notif = new SendNotification();
            var emailSetting = await unitOfWork.SettingRepository.Get<AddEmailSetting>(SettingEnum.EmailSetting.EnumToString(), cancellationToken);
            NotificationEmail emailSend = new NotificationEmail(notif);
         //   var sendEmail = await emailSend.Send(emailSetting.Result.From, "Email Confirm Code", content, emailSetting.Result.Username, emailSetting.Result.Password,MailboxAddress, emailSetting.Result.Port, emailSetting.Result.SmtpServer);
            if (true)
            {
                return OperationResult<string>.BuildSuccessResult("Success Send Email");
            }
            return OperationResult<string>.BuildFailure("Fail Send Email");
        }
    }
}
