using Common.Operation;
using DataTransfer.RoleDtos;
using MediatR;
using SiteService.Repositories.Implementation;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Query.RoleQueries
{
    public class GetUserRoleQueryHandler : IRequestHandler<GetUserRoleQuery, OperationResult<GetUserRoleDto>>
    {
        private readonly IDomainUnitOfWork unitOfWork;

        public GetUserRoleQueryHandler(IDomainUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }
        public async Task<OperationResult<GetUserRoleDto>> Handle(GetUserRoleQuery request, CancellationToken cancellationToken)
        {
            var userRole = await unitOfWork.UsersRepository.UsersRoleRepository.GetByUserId(request.UserId);
            if (userRole.Result != null)
            {
                var roles = await unitOfWork.RoleRepository.GetAllRole(cancellationToken);
                if (roles.Result != null)
                {
                    return OperationResult<GetUserRoleDto>.BuildSuccessResult(new GetUserRoleDto
                    {
                        CurrenrRoleId = userRole.Result.RoleId,
                        Roles = roles.Result
                    });
                }
                return OperationResult<GetUserRoleDto>.BuildFailure("Role Not Found");
            }
            return OperationResult<GetUserRoleDto>.BuildFailure("User not Found");
        }
    }
}
