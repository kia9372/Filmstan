using Common.Operation;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Command.CategoryCommands
{
    public class CreateCategoryCommand : IRequest<OperationResult<string>>
    {
        public string Name { get; set; }
        public Guid? ParentId { get; set; }
    }
}
