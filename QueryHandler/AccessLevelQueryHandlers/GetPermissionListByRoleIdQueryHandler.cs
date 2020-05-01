using Common.FilmstanExtentions;
using Common.Operation;
using DataTransfer.ControllerDtos;
using MediatR;
using Query.AccessLevelQueries;
using SiteService.Repositories.Implementation;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace QueryHandler.AccessLevelQueryHandlers
{
    public class GetPermissionListByRoleIdQueryHandler : IRequestHandler<GetPermissionListByRoleIdQuery, OperationResult<IEnumerable<ControllerDto>>>
    {
        private readonly IDomainUnitOfWork unitOfWork;

        public GetPermissionListByRoleIdQueryHandler(IDomainUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }
        public async Task<OperationResult<IEnumerable<ControllerDto>>> Handle(GetPermissionListByRoleIdQuery request, CancellationToken cancellationToken)
        {
            var accessList = unitOfWork.RoleRepository.AccessLevelRepository.GetAccessLevels(request.RoleId);
            return OperationResult<IEnumerable<ControllerDto>>.BuildSuccessResult(accessList.FindSelectedAccess());
        }
    }
}
