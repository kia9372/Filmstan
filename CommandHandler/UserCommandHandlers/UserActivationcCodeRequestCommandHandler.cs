using Command.UserCommands;
using Common.FilmStanEnums;
using Common.FilmstanExtentions;
using Common.Operation;
using Common.Utilitis;
using DataTransfer.Setting;
using Domain.Aggregate.DomainAggregates.UserAggregate;
using Domain.Aggregate.DomainAggregates.UserAggregate.ValueObjects;
using MediatR;
using SiteService.Repositories.Implementation;
using SiteService.SendNotificationStrategy;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CommandHandler.UserCommandHandlers
{
    public class UserActivationcCodeRequestCommandHandler : IRequestHandler<UserActivationcCodeRequestCommand, OperationResult<string>>
    {
        private readonly IDomainUnitOfWork unitOfWork;
        private readonly IEnumerable<SendNotif> sendNotifs;

        public UserActivationcCodeRequestCommandHandler(IDomainUnitOfWork unitOfWork, IEnumerable<SendNotif> sendNotifs)
        {
            this.unitOfWork = unitOfWork;
            this.sendNotifs = sendNotifs;
        }
        public async Task<OperationResult<string>> Handle(UserActivationcCodeRequestCommand request, CancellationToken cancellationToken)
        {
            var result = await unitOfWork.UsersRepository.GetUserByPhoneNumberAsync(request.PhoneNumber, cancellationToken);
            if (result.Result != null)
            {
                ActivationCode acCode = new ActivationCode(result.Result.Id, CodeTypes.PhoneConfirmed, Utility.Hash(result.Result.Username));
                var gerateActivationCode = await unitOfWork.UsersRepository.ActivationCodeRepository.AddAsync(acCode, cancellationToken);
                if (gerateActivationCode.Success)
                {
                    var registerSetting = await unitOfWork.SettingRepository.Get<RegisterUserSetting>(SettingEnum.RegisterUserSetting.EnumToString(), cancellationToken);
                    if (registerSetting.Success)
                    {
                        SendNotif sendNotif = sendNotifs.Where(x => x.SendCodeType == registerSetting.Result.SendCodeVerifications)
                                   .FirstOrDefault();
                        var sendCode = await sendNotif.SendCodeAsync(gerateActivationCode.Result.Item2.ToString(), request.PhoneNumber, cancellationToken);
                        if (sendCode.Success)
                        {
                            /// Save to Database
                            await unitOfWork.CommitSaveChangeAsync();
                            return OperationResult<string>.BuildSuccessResult(gerateActivationCode.Result.Item1);
                        }
                        return OperationResult<string>.BuildFailure(sendCode.ErrorMessage);
                    }
                    return OperationResult<string>.BuildFailure(registerSetting.ErrorMessage);
                }
                return OperationResult<string>.BuildFailure(gerateActivationCode.ErrorMessage);
            }
            return OperationResult<string>.BuildFailure("User Not found");
        }
    }
}
