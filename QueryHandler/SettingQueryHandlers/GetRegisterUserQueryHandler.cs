using Common.FilmStanEnums;
using Common.FilmstanExtentions;
using Common.Operation;
using DataTransfer.Setting;
using MediatR;
using Query.SettingQueries;
using SiteService.Repositories.Implementation;
using System.Threading;
using System.Threading.Tasks;

namespace QueryHandler.SettingQueryHandlers
{
    public class GetRegisterUserQueryHandler : IRequestHandler<RegisterUserSettingQuery, OperationResult<RegisterUserSetting>>
    {
        private readonly IDomainUnitOfWork unitOfWork;

        public GetRegisterUserQueryHandler(IDomainUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }
        public async Task<OperationResult<RegisterUserSetting>> Handle(RegisterUserSettingQuery request, CancellationToken cancellationToken)
        {
            var result = await unitOfWork.SettingRepository.Get<RegisterUserSetting>(SettingEnum.RegisterUserSetting.EnumToString(), cancellationToken);
            if (result.Result != null)
            {
                return OperationResult<RegisterUserSetting>.BuildSuccessResult(result.Result);
            }
            return OperationResult<RegisterUserSetting>.BuildFailure("Empty");
        }
    }
}
