using Command.SettingCommand;
using Common.Operation;
using MediatR;
using SiteService.Repositories.Implementation;
using System.Threading;
using System.Threading.Tasks;

namespace BehaviorHandler.PipeLineBehaviors.SettingSiteBehavior
{
    public class CheckRoleRegisterByAdminValidation<TRequest, TResponse> : IPipelineBehavior<SetRegisterUserSettingCommand, OperationResult<string>>
    {
        private readonly IDomainUnitOfWork unitOfWork;

        public CheckRoleRegisterByAdminValidation(IDomainUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }
        public async Task<OperationResult<string>> Handle(SetRegisterUserSettingCommand request, CancellationToken cancellationToken, RequestHandlerDelegate<OperationResult<string>> next)
        {
            var findRegisterAdminRole = await unitOfWork.RoleRepository.GetRoleByIdAsync(request.RegisterRoleByAdmin, cancellationToken);
            if (findRegisterAdminRole.Result == null)
            {
                return OperationResult<string>.BuildFailure("RegisterRoleByAdmin not valid role");
            }
            return await next();
        }
    }
}
