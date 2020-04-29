using Common.Operation;
using MediatR;
using System;

namespace Command.PostMagazinrCommands
{
    public class DeletetPostMagazineCommands : IRequest<OperationResult<string>>
    {
        public Guid id { get; set; }
    }
}
