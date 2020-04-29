using Common.Operation;
using MediatR;
using System;

namespace Command.UserRoleCommands
{
    public class UpdateUserRoleCommand : IRequest<OperationResult<bool>>
    {
        public Guid RoleId { get; private set; }
        public Guid UserId { get; private set; }
        public UpdateUserRoleCommand(Guid roleId, Guid userId)
        {
            RoleId = roleId;
            UserId = userId;
        }
    }
}
