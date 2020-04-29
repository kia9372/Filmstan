using Common.Operation;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Command.UserRoleCommands
{
    public class CreateUserRoleCommand : IRequest<OperationResult<bool>>
    {
        public Guid RoleId { get; private set; }
        public Guid UserId { get; private set; }
        public CreateUserRoleCommand(Guid roleId, Guid userId)
        {
            RoleId = roleId;
            UserId = userId;
        }
    }
}
