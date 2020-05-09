using Common.Operation;
using MediatR;
using System;

namespace Command.UserCommands
{
    public class ChangeUserActiveStatusCommand : IRequest<OperationResult<bool>>
    {
        public Guid Id { get; set; }
    }

    public class ChangeUserEmailStatusCommand : IRequest<OperationResult<bool>>
    {
        public Guid Id { get; set; }
    }
    public class ChangeUserLockEndStatusCommand : IRequest<OperationResult<bool>>
    {
        public Guid Id { get; set; }
    }

    public class ChangeUserPhoneConfirmedStatusCommand : IRequest<OperationResult<bool>>
    {
        public Guid Id { get; set; }
    }
}
