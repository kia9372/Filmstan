using Command.UserRoleCommands;
using Common.Operation;
using MediatR;
using SiteService.Repositories.Implementation;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace CommandHandler.UserRoleCommandHandlers
{
    public class UpdateUserRoleCommandHandler : IRequestHandler<UpdateUserRoleCommand, OperationResult<bool>>
    {
        private readonly IDomainUnitOfWork unitOfWork;

        public UpdateUserRoleCommandHandler(IDomainUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }
        public async Task<OperationResult<bool>> Handle(UpdateUserRoleCommand request, CancellationToken cancellationToken)
        {
            var getUserRole = await unitOfWork.UsersRepository.UsersRoleRepository.GetByUserId(request.RoleId);
            if (getUserRole.Result != null)
            {
                getUserRole.Result.SetValues(request.RoleId, request.UserId);
                var updateUserRole = unitOfWork.UsersRepository.UsersRoleRepository.UpdateUserRole(getUserRole.Result); 
                if (updateUserRole.Success)
                {
                    try
                    {
                        await unitOfWork.CommitSaveChangeAsync();
                    }
                    catch (Exception ex)
                    {
                        return OperationResult<bool>.BuildFailure(ex);
                    }
                    return OperationResult<bool>.BuildSuccessResult(true);
                }
                return OperationResult<bool>.BuildFailure(updateUserRole.ErrorMessage);
            }
            return OperationResult<bool>.BuildFailure(getUserRole.ErrorMessage);
        }
    }

}
