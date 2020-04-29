using Common.Operation;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Command.RoleCommands
{
    public class DeleteRoleCommand : IRequest<OperationResult<string>>
    {
        public Guid Id { get; private set; }
        public DeleteRoleCommand(Guid id)
        {
            Id = id;
        }
    }
}