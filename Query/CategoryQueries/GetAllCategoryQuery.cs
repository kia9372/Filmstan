using Common.Operation;
using Domain.Aggregate.DomainAggregates.CategoryAggregate;
using MediatR;
using System.Collections.Generic;
using System.Text;

namespace Query.CategoryQueries
{
    public class GetAllCategoryQuery : IRequest<OperationResult<IEnumerable<Category>>>
    {
    }
}
