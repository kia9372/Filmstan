using Common.Operation;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Command.RoleCommands
{
    public class CreateRoleCommand : IRequest<OperationResult<string>>
    {
        public string Name { get; private set; }
        public string Description { get; private set; }
        public CreateRoleCommand(string name, string description)
        {
            Name = name ?? throw new ArgumentNullException(nameof(name));
            Description = description ?? throw new ArgumentNullException(nameof(description));
        }
    }
}
