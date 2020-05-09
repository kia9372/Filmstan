using Common.Operation;
using MediatR;
using System;
using System.Collections;

namespace Command.CategoryPropertyCommands
{
    public class DeleteCategoryPropertyCommand : IRequest<OperationResult<string>>
    {
        public Guid CategoryPropertyId { get; set; }
    }
}
