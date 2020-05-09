using Common.Operation;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Command.BrandCommands
{
    public class CreateBrandCommand : IRequest<OperationResult<string>>
    {
        public Guid CategoryId { get; set; }
        public string ISOBrandName { get; set; }
        public string BrandName { get; set; }
    }
}
