using Common.Operation;
using MediatR;

namespace Command.UserCommands
{
    public class ConfirmedPhoneNumberUerCommand : IRequest<OperationResult<bool>>
    {
        public string PhoneNumber { get; private set; }
        public ConfirmedPhoneNumberUerCommand(string phoneNumber)
        {
            PhoneNumber = phoneNumber;
        }
    }
}
