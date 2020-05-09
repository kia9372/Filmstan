using Common.Operation;
using Domain.Aggregate.DomainAggregates.CategoryAggregate;
using MediatR;
using System;
using System.Text;

namespace Query.BrandQueries
{
    public class GetBrandById : IRequest<OperationResult<Brand>>
    {
        public Guid brandId { get; set; }
    }
}
