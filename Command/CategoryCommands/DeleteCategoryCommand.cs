using Common.Operation;
using MediatR;
using System;

namespace Command.CategoryCommands
{
    public class DeleteCategoryCommand : IRequest<OperationResult<string>>
    {
        public Guid Id { get; set; }
    }
}
