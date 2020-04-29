using Command.RoleCommands;
using Common.Operation;
using MediatR;
using SiteService.Repositories.Implementation;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace CommandHandler.RoleCommandHandler
{
    public class UpdateRoleCommandHandler : IRequestHandler<UpdateRoleCommand, OperationResult<string>>
    {
        private readonly IDomainUnitOfWork unitOfWork;

        public UpdateRoleCommandHandler(IDomainUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }
        public async Task<OperationResult<string>> Handle(UpdateRoleCommand request, CancellationToken cancellationToken)
        {
            var role = await unitOfWork.RoleRepository.GetRoleByIdAsync(request.Id, cancellationToken);
            if (role.Result != null)
            {
                role.Result.SetProperties(request.Name, request.Description);
                var update = unitOfWork.RoleRepository.Update(role.Result, cancellationToken);
                if (update.Success)
                {
                    try
                    {
                        await unitOfWork.CommitSaveChangeAsync();
                        return OperationResult<string>.BuildSuccessResult("Success Update");
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
