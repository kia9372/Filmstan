using Common.Operation;
using Domain.Aggregate.DomainAggregates.PostMagAggregate;
using MediatR;
using System;
using System.Text;

namespace Query.PostMagazaineQueries
{
    public class GetPostByIdQuery : IRequest<OperationResult<PostMagazine>>
    {
        public Guid Id { get; set; }
    }
}
