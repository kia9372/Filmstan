using Command.RoleCommands;
using Common.Operation;
using MediatR;
using SiteService.Repositories.Implementation;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CommandHandler.RoleCommandHandler
{
    public class DeleteRoleCommandHandler : IRequestHandler<DeleteRoleCommand, OperationResult<string>>
    {
        private readonly IDomainUnitOfWork unitOfWork;

        public DeleteRoleCommandHandler(IDomainUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }
        public async Task<OperationResult<string>> Handle(DeleteRoleCommand request, CancellationToken cancellationToken)
        {
            var role = await unitOfWork.RoleRepository.GetRoleByIdAsync(request.Id, cancellationToken);
            if (role.Result != null)
            {
                role.Result.Delete();
                role.Result.UpdateSecurityStamp();
                var update = unitOfWork.RoleRepository.Update(role.Result, cancellationToken);
                if (update.Success)
                {
                    try
                    {
                        await unitOfWork.CommitSaveChangeAsync();
                        return OperationResult<string>.BuildSuccessResult("Success Delete");
                    }
                    catch (Exception ex)
                    {
                        return OperationResult<string>.BuildFailure(ex);
                    }
                }
            }
            return OperationResult<string>.BuildFailure(role.ErrorMessage);
        }
    }
}
