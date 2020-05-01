using Common.Operation;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Command.UserCommands
{
    public class UserActivationcCodeRequestCommand : IRequest<OperationResult<string>>
    {
        public string PhoneNumber { get; set; }
    }
}
