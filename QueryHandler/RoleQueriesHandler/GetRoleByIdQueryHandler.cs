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
    public class GetRoleByIdQueryHandler : IRequestHandler<GetRoleByIdQuery, OperationResult<Role>>
    {
        private readonly IDomainUnitOfWork unitOfWork;

        public GetRoleByIdQueryHandler(IDomainUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }
        public async Task<OperationResult<Role>> Handle(GetRoleByIdQuery request, CancellationToken cancellationToken)
        {
            var role = await unitOfWork.RoleRepository.GetRoleByIdAsync(request.Id, cancellationToken);
            if (role.Result != null)
            {
                return OperationResult<Role>.BuildSuccessResult(role.Result);
            }
            return OperationResult<Role>.BuildFailure(role.ErrorMessage);
        }
    }
}
