using Common.Operation;
using Domain.Aggregate.DomainAggregates.RoleAggregate;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Query.RoleQueries
{
    public class GetRoleListQuery : IRequest<OperationResult<IEnumerable<Role>>>
    {
        public GetRoleListQuery()
        {

        }
    }
}
