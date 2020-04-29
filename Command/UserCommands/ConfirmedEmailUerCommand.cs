using Common.Operation;
using MediatR;

namespace Command.UserCommands
{
    public class ConfirmedEmailUerCommand : IRequest<OperationResult<bool>>
    {
        public string HashCode { get; set; }
        public int Code { get; set; }
    }
}
