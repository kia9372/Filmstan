using Common.Operation;
using Domain.Aggregate.DomainAggregates.CategoryAggregate;
using MediatR;
using System;

namespace Query.CategoryQueries
{
    public class GetCategoryByIdQuery : IRequest<OperationResult<Category>>
    {
        public Guid Id { get; set; }
    }
}
