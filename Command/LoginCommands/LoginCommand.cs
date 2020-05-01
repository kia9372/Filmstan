using Common.Operation;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Command.LoginCommands
{
    public class LoginCommand : IRequest<OperationResult<string>>
    {
        public string Username { get; set; }
        public string Password { get; set; }
    }
}
