using Common.Operation;
using DataTransfer;
using DataTransfer.RoleDtos;
using Domain.Aggregate.DomainAggregates.CategoryAggregate;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Query.CategoryQueries
{
    public class GetAllCategoryPagingQuery : GetAllFormQuery, IRequest<OperationResult<GetAllPaging<Category>>>
    {
    }
}
