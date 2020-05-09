using Common.Operation;
using MediatR;
using System;

namespace Command.BrandCommands
{
    public class DeleteBrandCommand : IRequest<OperationResult<string>>
    {
        public Guid BrandId { get; set; }
    }
}
