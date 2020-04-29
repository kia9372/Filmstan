using Common.Operation;
using Domain.Aggregate.DomainAggregates.RoleAggregate;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Query.RoleQueries
{
    public class GetRoleByIdQuery :IRequest<OperationResult<Role>>
    {
        public Guid Id { get; private set; }
        public GetRoleByIdQuery(Guid id)
        {
            Id = id;
        }
    }
}
