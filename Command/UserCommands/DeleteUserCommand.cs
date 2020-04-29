using Common.Operation;
using MediatR;
using System;

namespace Command.UserCommands
{
    public class DeleteUserCommand : IRequest<OperationResult<bool>>
    {
        public Guid Id { get; private set; }
        public DeleteUserCommand(Guid id)
        {
            Id = Id;
        }
    }
}
