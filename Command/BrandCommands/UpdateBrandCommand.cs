using Common.Operation;
using MediatR;
using System;

namespace Command.BrandCommands
{
    public class UpdateBrandCommand : IRequest<OperationResult<string>>
    {
        public Guid BrandId { get; set; }
        public string ISOBrandName { get; set; }
        public Guid CategoryId { get; set; }
        public string BrandName { get; set; }
    }
}
