using Common.Operation;
using MediatR;
using System;

namespace Command.UserCommands
{
    public class ChangePhoneNumberUserCommand : IRequest<OperationResult<bool>>
    {
        public Guid Id { get; set; }
        public string PhoneNumber { get; set; }
    }
}
