using Common.FilmStanEnums;
using Common.FilmstanExtentions;
using Common.Notification;
using Common.Operation;
using Common.SiteEnums;
using DataTransfer.EmailSettingDtos;
using DataTransfer.SMSSettingDtos;
using SiteService.Repositories.Implementation;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SiteService.SendNotificationStrategy
{
    public class SendNotifByBoth : SendNotif
    {
        private readonly IDomainUnitOfWork unitOfWork;

        public SendNotifByBoth(IDomainUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }
        public override SendCodeVerification SendCodeType => SendCodeVerification.Both;
        public override async Task<OperationResult<string>> SendCodeAsync(string content, string to, CancellationToken cancellationToken)
        {
            SendNotification notif = new SendNotification();
            var smsSetting = await unitOfWork.SettingRepository.Get<AddSMSSetting>(SettingEnum.SMSSetting.EnumToString(), cancellationToken);
            var emailSetting = await unitOfWork.SettingRepository.Get<AddEmailSetting>(SettingEnum.EmailSetting.EnumToString(), cancellationToken);
            NotificationSms smsSend = new NotificationSms(notif);
            NotificationEmail emailSend = new NotificationEmail(smsSend);
            var sendSms = await smsSend.Send(smsSetting.Result.LineNumber, smsSetting.Result.userApikey, to, content, smsSetting.Result.secretKey);
            if (sendSms.Success)
            {
                // var sendEmail = await emailSend.Send(emailSetting.Result.From, "Email Confirm Code", content, emailSetting.Result.Username, emailSetting.Result.Password, to, emailSetting.Result.Port, emailSetting.Result.SmtpServer);
            }
            return OperationResult<string>.BuildFailure("Fail Send SMS");
        }
    }
}
