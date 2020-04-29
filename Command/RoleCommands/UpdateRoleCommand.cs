using Common.Operation;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Command.RoleCommands
{
    public class UpdateRoleCommand : IRequest<OperationResult<string>>
    {
        public string Name { get; private set; }
        public string Description { get; private set; }
        public Guid Id { get; private set; }
        public UpdateRoleCommand(string name, string description, Guid id)
        {
            Name = name ?? throw new ArgumentNullException(nameof(name));
            Description = description ?? throw new ArgumentNullException(nameof(description));
            Id = id;
        }
    }
}