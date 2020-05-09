using Common.Operation;
using Domain.Aggregate.DomainAggregates.CategoryAggregate;
using MediatR;
using System;
using System.Collections.Generic;

namespace Query.BrandQueries
{
    public class GetBrandByCategoryId : IRequest<OperationResult<IEnumerable<Brand>>>
    {
        public Guid categoryId { get; set; }
    }
}
