using Command.SettingCommand;
using Common.FilmStanEnums;
using Common.Operation;
using DataTransfer.EmailSettingDtos;
using Domain.Aggregate.DomainAggregates.SettingAggregate;
using MediatR;
using SiteService.Repositories.Implementation;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace CommandHandler.SettingCommandHandler.SetEmailSettingCommandHandler
{
    public class CreateEmailSettingHandler : IRequestHandler<CreateEmailSettingCommand, OperationResult<string>>
    {
        private readonly IDomainUnitOfWork repository;

        public CreateEmailSettingHandler(IDomainUnitOfWork repository)
        {
            this.repository = repository;
        }
        public async Task<OperationResult<string>> Handle(CreateEmailSettingCommand request, CancellationToken cancellationToken)
        {
            var findSetting = await repository.SettingRepository.Set<AddEmailSetting>(Enum.GetName(typeof(SettingEnum), SettingEnum.EmailSetting), request, cancellationToken);
            if (findSetting.Success)
            {
                try
                {
                    await repository.CommitSaveChangeAsync();
                    return OperationResult<string>.BuildSuccessResult("Success Set Email Settings");
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
