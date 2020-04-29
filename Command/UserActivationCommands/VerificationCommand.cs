using Common.Operation;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Command.UserActivationCommands
{
    public class VerificationCommand : IRequest<OperationResult<string>>
    {
        public string HashCode { get; set; }
        public int Code { get; set; }
    }
}
