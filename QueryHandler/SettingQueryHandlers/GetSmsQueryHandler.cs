using Common.FilmStanEnums;
using Common.FilmstanExtentions;
using Common.Operation;
using DataTransfer.SMSSettingDtos;
using MediatR;
using Query.SettingQueries;
using SiteService.Repositories.Implementation;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace QueryHandler.SettingQueryHandlers
{
    public class GetSmsQueryHandler : IRequestHandler<SmsSettingQuery, OperationResult<AddSMSSetting>>
    {
        private readonly IDomainUnitOfWork unitOfWork;

        public GetSmsQueryHandler(IDomainUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }
        public async Task<OperationResult<AddSMSSetting>> Handle(SmsSettingQuery request, CancellationToken cancellationToken)
        {
            var result = await unitOfWork.SettingRepository.Get<AddSMSSetting>(SettingEnum.SMSSetting.EnumToString(), cancellationToken);
            if (result.Result != null)
            {
                return OperationResult<AddSMSSetting>.BuildSuccessResult(result.Result);
            }
            return OperationResult<AddSMSSetting>.BuildFailure("Empty");
        }
    }
}
