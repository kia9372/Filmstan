using Command.UserCommands;
using Common.Operation;
using MediatR;
using SiteService.Repositories.Implementation;
using System.Threading;
using System.Threading.Tasks;

namespace BehaviorHandler.PipeLineBehaviors.RegisterUserBehavior
{
    public class CheckPhoneNumbrExistValidation<TRequest, TResponse> : IPipelineBehavior<CreateUserCommand, OperationResult<string>>
    {
        private readonly IDomainUnitOfWork unitOfWork;

        public CheckPhoneNumbrExistValidation(IDomainUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }
        public async Task<OperationResult<string>> Handle(CreateUserCommand request, CancellationToken cancellationToken, RequestHandlerDelegate<OperationResult<string>> next)
        {
            var findUserName = await unitOfWork.UsersRepository.GetUserByPhoneNumberAsync(request.PhoneNumber, cancellationToken);
            if (findUserName.Result != null)
            {
                return OperationResult<string>.BuildFailure("phoneNumber Exist");
            }
            return await next();
        }
    }
}
