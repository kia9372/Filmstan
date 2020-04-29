using Common.Operation;
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
    public class GetRoleListQueryHandler : IRequestHandler<GetRoleListQuery, OperationResult<IEnumerable<Role>>>
    {
        private readonly IDomainUnitOfWork unitOfWork;

        public GetRoleListQueryHandler(IDomainUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }
        public async Task<OperationResult<IEnumerable<Role>>> Handle(GetRoleListQuery request, CancellationToken cancellationToken)
        {
            var role = await unitOfWork.RoleRepository.GetAllRoleAsync(cancellationToken);
            if (role.Result != null)
            {
                return OperationResult<IEnumerable<Role>>.BuildSuccessResult(role.Result);
            }
            return OperationResult<IEnumerable<Role>>.BuildFailure(role.ErrorMessage);
        }
    }
}
