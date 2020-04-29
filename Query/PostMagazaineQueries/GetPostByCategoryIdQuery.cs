using Common.Operation;
using Domain.Aggregate.DomainAggregates.PostMagAggregate;
using MediatR;
using System;
using System.Collections.Generic;

namespace Query.PostMagazaineQueries
{
    public class GetPostByCategoryIdQuery : IRequest<OperationResult<IEnumerable<PostMagazine>>>
    {
        public Guid categoryId { get; set; }
    }
}
