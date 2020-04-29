using Command.SettingCommand;
using Common.Operation;
using MediatR;
using SiteService.Repositories.Implementation;
using System.Threading;
using System.Threading.Tasks;

namespace BehaviorHandler.PipeLineBehaviors.SettingSiteBehavior
{
    public class CheckRoleRegisterByUserValidation<TRequest, TResponse> : IPipelineBehavior<SetRegisterUserSettingCommand, OperationResult<string>>
    {
        private readonly IDomainUnitOfWork unitOfWork;

        public CheckRoleRegisterByUserValidation(IDomainUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }
        public async Task<OperationResult<string>> Handle(SetRegisterUserSettingCommand request, CancellationToken cancellationToken, RequestHandlerDelegate<OperationResult<string>> next)
        {
            var findRegisterUserRole = await unitOfWork.RoleRepository.GetRoleByIdAsync(request.RegisterRoleByUser, cancellationToken);
            if (findRegisterUserRole.Result == null)
            {
                return OperationResult<string>.BuildFailure("RegisterRoleByUser not valid role");
            }
            return await next();
        }
    }
}
