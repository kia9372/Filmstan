using Common.Operation;
using MediatR;
using System;

namespace Command.CategoryCommands
{
    public class UpdateCategoryCommand : IRequest<OperationResult<string>>
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public Guid? ParentId { get; set; }
    }
}
