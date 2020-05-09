using Common.Operation;
using Domain.Aggregate.DomainAggregates.CategoryAggregate;
using MediatR;
using System;
using System.Collections.Generic;

namespace Command.CategoryPropertyCommands
{
    public class GetAllCategoryPropertyCommand : IRequest<OperationResult<IEnumerable<CategoryProperty>>>
    {
        public Guid CategoryPropertyId { get; set; }
    }
}
