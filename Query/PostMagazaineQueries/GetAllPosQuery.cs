using Common.Operation;
using Domain.Aggregate.DomainAggregates.PostMagAggregate;
using MediatR;
using System.Collections.Generic;

namespace Query.PostMagazaineQueries
{
    public class GetAllPosQuery : IRequest<OperationResult<IEnumerable<PostMagazine>>>
    {
    }
}
