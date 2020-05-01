using Command.AccessLevelCommands;
using Common.Operation;
using DataTransfer.RoleDtos;
using MediatR;
using SiteService.Repositories.Implementation;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CommandHandler.AccessLevelCommandHandlers
{
    public class SetAccessLevelCommandHandler : IRequestHandler<SetAccessLevelCommand, OperationResult<string>>
    {
        private readonly IDomainUnitOfWork unitOfWork;

        public SetAccessLevelCommandHandler(IDomainUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }
        public async Task<OperationResult<string>> Handle(SetAccessLevelCommand request, CancellationToken cancellationToken)
        {
            var result = await unitOfWork.RoleRepository.AccessLevelRepository.SetAccess(new AccessLevelDto { RoleId = request.RoleId, Access = request.AccessList });
            if (result.Success)
            {
                try
                {
                    var findRole = await unitOfWork.RoleRepository.GetRoleByIdAsync(request.RoleId, cancellationToken);
                    findRole.Result.UpdateSecurityStamp();
                    if (findRole.Result != null)
                    {
                        unitOfWork.RoleRepository.Update(findRole.Result, cancellationToken);
                        await unitOfWork.CommitSaveChangeAsync();
                        return OperationResult<string>.BuildSuccessResult("Add Success");
                    }
                }
                catch (Exception ex)
                {
                    return OperationResult<string>.BuildFailure(ex.Message);
                }

            }
            return OperationResult<string>.BuildFailure(result.ErrorMessage);
        }
    }
}
