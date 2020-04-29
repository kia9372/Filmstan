using Common.Operation;
using MediatR;
using System;

namespace Command.UserCommands
{
    public class ChangeUserActiveStatusCommand : IRequest<OperationResult<bool>>
    {
        public Guid Id { get; set; }
    }
}
