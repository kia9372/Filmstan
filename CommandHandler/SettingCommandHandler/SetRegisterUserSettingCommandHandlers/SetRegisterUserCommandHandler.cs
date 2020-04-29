using Command.SettingCommand;
using Common.FilmStanEnums;
using Common.Operation;
using DataTransfer.Setting;
using Domain.Aggregate.DomainAggregates.SettingAggregate;
using MediatR;
using SiteService.Repositories.Implementation;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CommandHandler.SettingCommandHandler.SetRegisterUserSettingCommandHandlers
{
    public class SetRegisterUserSettingHandler : IRequestHandler<SetRegisterUserSettingCommand, OperationResult<string>>
    {
        private readonly IDomainUnitOfWork repository;

        public SetRegisterUserSettingHandler(IDomainUnitOfWork repository)
        {
            this.repository = repository;
        }
        public async Task<OperationResult<string>> Handle(SetRegisterUserSettingCommand request, CancellationToken cancellationToken)
        {
            var findSetting = await repository.SettingRepository.Set<RegisterUserSetting>(Enum.GetName(typeof(SettingEnum), SettingEnum.RegisterUserSetting), request, cancellationToken);
            if (findSetting.Success)
            {
                try
                {
                    await repository.CommitSaveChangeAsync();
                    return OperationResult<string>.BuildSuccessResult("Success Set Register User Settings");
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
