using Common.FilmStanEnums;
using Common.FilmstanExtentions;
using Common.Notification;
using Common.Operation;
using Common.SiteEnums;
using DataTransfer.SMSSettingDtos;
using Domain.Aggregate.DomainAggregates.UserAggregate;
using SiteService.Repositories.Implementation;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SiteService.SendNotificationStrategy
{
    public class SendNotifBySms : SendNotif
    {
        private readonly IDomainUnitOfWork unitOfWork;

        public SendNotifBySms(IDomainUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }
        public override SendCodeVerification SendCodeType => SendCodeVerification.Sms;
        public override async Task<OperationResult<string>> SendCodeAsync(string content, string to, CancellationToken cancellationToken)
        {
            SendNotification notif = new SendNotification();
            var smsSetting = await unitOfWork.SettingRepository.Get<AddSMSSetting>(SettingEnum.SMSSetting.EnumToString(), cancellationToken);
            NotificationSms smsSend = new NotificationSms(notif);
            var sendSms = await smsSend.Send(smsSetting.Result.LineNumber, smsSetting.Result.userApikey, to, content, smsSetting.Result.secretKey);
            if (sendSms.Success)
            {
                return OperationResult<string>.BuildSuccessResult("Success Send SMS");
            }
            return OperationResult<string>.BuildFailure("Fail Send SMS");
        }
    }
}
