using Common.Operation;
using Domain.Aggregate.DomainAggregates.PostMagAggregate;
using MediatR;
using System;
using System.Collections.Generic;

namespace Query.PostMagazaineQueries
{
    public class GetPostByWriteIdQuery : IRequest<OperationResult<IEnumerable<PostMagazine>>>
    {
        public Guid writerId { get; set; }
    }
}
