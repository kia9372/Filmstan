using Common.Operation;
using DataTransfer.CategoryPropertyDto;
using Domain.Core.Shared;
using MediatR;
using System.Collections.Generic;
using System.Text;

namespace Command.CategoryPropertyCommands
{
    public class CreateCategoryPropertyCommand : IRequest<OperationResult<string>>
    {
       public List<CategoryPropertyDto> CategoryPropertyDtos { get; set; }
    }
}
