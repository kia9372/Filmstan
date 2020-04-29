using Command.SmsSettingCommand;
using Common.FilmStanEnums;
using Common.Operation;
using DataTransfer.SMSSettingDtos;
using Domain.Aggregate.DomainAggregates.SettingAggregate;
using MediatR;
using SiteService.Repositories.Implementation;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CommandHandler.SettingCommandHandler.SmsSettingCommandHandler
{
    public class SetSmsSettingCommandHandler : IRequestHandler<SetSmsSettingCommand, OperationResult<string>>
    {
        private readonly IDomainUnitOfWork repository;

        public SetSmsSettingCommandHandler(IDomainUnitOfWork repository)
        {
            this.repository = repository;
        }
        public async Task<OperationResult<string>> Handle(SetSmsSettingCommand request, CancellationToken cancellationToken)
        {
            var findSetting = await repository.SettingRepository.Set<AddSMSSetting>(Enum.GetName(typeof(SettingEnum), SettingEnum.SMSSetting), request, cancellationToken);
            if (findSetting.Success)
            {
                try
                {
                    await repository.CommitSaveChangeAsync();
                    return OperationResult<string>.BuildSuccessResult("Success Set Sms Settings");
                }
                catch (Exception ex)
                {
                    return OperationResult<string>.BuildFailure(ex.Message);
                }
            }
            return OperationResult<string>.BuildFailure(findSetting.ErrorMessage);
        }
    }
}

