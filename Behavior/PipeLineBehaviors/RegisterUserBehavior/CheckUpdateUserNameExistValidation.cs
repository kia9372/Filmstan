using Command.UserCommands;
using Common.Operation;
using MediatR;
using SiteService.Repositories.Implementation;
using System.Threading;
using System.Threading.Tasks;

namespace BehaviorHandler.PipeLineBehaviors.RegisterUserBehavior
{
    public class CheckUpdateUserNameExistValidation<TRequest, TResponse> : IPipelineBehavior<UpdateUserCommand, OperationResult<string>>
    {
        private readonly IDomainUnitOfWork unitOfWork;

        public CheckUpdateUserNameExistValidation(IDomainUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }
        public async Task<OperationResult<string>> Handle(UpdateUserCommand request, CancellationToken cancellationToken, RequestHandlerDelegate<OperationResult<string>> next)
        {
            var findUserName = await unitOfWork.UsersRepository.GetUserByUsernameAsync(request.Username, cancellationToken);
            if (findUserName.Result != null)
            {
                return OperationResult<string>.BuildFailure("UserName Exist");
            }
            return await next();
        }
    }
}
