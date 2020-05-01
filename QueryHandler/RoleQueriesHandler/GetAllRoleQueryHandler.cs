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
    public class GetAllRoleQueryHandler : IRequestHandler<GetAllRoleQuery, OperationResult<IEnumerable<Role>>>
    {
        private readonly IDomainUnitOfWork unitOfWork;

        public GetAllRoleQueryHandler(IDomainUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }
        public async Task<OperationResult<IEnumerable<Role>>> Handle(GetAllRoleQuery request, CancellationToken cancellationToken)
        {
            var result = await unitOfWork.RoleRepository.GetAllRole(cancellationToken);
            if(result.Success)
            {
                return OperationResult<IEnumerable<Role>>.BuildSuccessResult(result.Result);
            }
            return OperationResult<IEnumerable<Role>>.BuildFailure(result.ErrorMessage);
        }
    }
}
