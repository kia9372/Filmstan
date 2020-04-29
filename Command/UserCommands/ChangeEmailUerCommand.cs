using Common.Operation;
using MediatR;
using System;

namespace Command.UserCommands
{
    public class ChangeEmailUerCommand : IRequest<OperationResult<bool>>
    {
        public Guid Id { get; set; }
        public string Email { get; set; }
    }
}
