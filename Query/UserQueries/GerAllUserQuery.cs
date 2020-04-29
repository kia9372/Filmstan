using Common.Operation;
using Domain.Aggregate.DomainAggregates.UserAggregate;
using MediatR;
using System.Collections.Generic;

namespace Query.UserQueries
{
    public class GerAllUserQuery : IRequest<OperationResult<IEnumerable<User>>>
    {
        public GerAllUserQuery()
        {
        }
    }
}
