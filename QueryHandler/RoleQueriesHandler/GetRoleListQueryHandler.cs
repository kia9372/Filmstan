using Common.Operation;
using DataTransfer;
using Domain.Aggregate.DomainAggregates.RoleAggregate;
using MediatR;
using Query.RoleQueries;
using SiteService.Repositories.Implementation;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace QueryHandler.RoleQueriesHandler
{
    public class GetRoleListQueryHandler : IRequestHandler<GetRoleListPagingQuery, OperationResult<GetAllPaging<Role>>>
    {
        private readonly IDomainUnitOfWork unitOfWork;

        public GetRoleListQueryHandler(IDomainUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }
        public async Task<OperationResult<GetAllPaging<Role>>> Handle(GetRoleListPagingQuery request, CancellationToken cancellationToken)
        {
            var role = await unitOfWork.RoleRepository.GetAllRolePagingAsync(request, cancellationToken);
            if (role.Result != null)
            {
                return OperationResult<GetAllPaging<Role>>.BuildSuccessResult(role.Result);
            }
            return OperationResult<GetAllPaging<Role>>.BuildFailure(role.ErrorMessage);
        }
    }
}
