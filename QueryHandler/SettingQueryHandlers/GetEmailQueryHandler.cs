using Common.FilmStanEnums;
using Common.FilmstanExtentions;
using Common.Operation;
using DataTransfer.EmailSettingDtos;
using MediatR;
using Query.SettingQueries;
using SiteService.Repositories.Implementation;
using System.Threading;
using System.Threading.Tasks;

namespace QueryHandler.SettingQueryHandlers
{
    public class GetEmailQueryHandler : IRequestHandler<EmailSettingQuery, OperationResult<AddEmailSetting>>
    {
        private readonly IDomainUnitOfWork unitOfWork;

        public GetEmailQueryHandler(IDomainUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }
        public async Task<OperationResult<AddEmailSetting>> Handle(EmailSettingQuery request, CancellationToken cancellationToken)
        {
            var result = await unitOfWork.SettingRepository.Get<AddEmailSetting>(SettingEnum.EmailSetting.EnumToString(), cancellationToken);
            if (result.Result != null)
            {
                return OperationResult<AddEmailSetting>.BuildSuccessResult(result.Result);
            }
            return OperationResult<AddEmailSetting>.BuildFailure("Empty");
        }
    }
}
