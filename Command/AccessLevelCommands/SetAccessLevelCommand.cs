using Common.Operation;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Command.AccessLevelCommands
{
    public class SetAccessLevelCommand : IRequest<OperationResult<string>>
    {
        public Guid RoleId { get; private set; }
        public List<string> AccessList { get; private set; }
        public SetAccessLevelCommand(Guid roleId, List<string> accessList)
        {
            RoleId = roleId;
            AccessList = accessList;
        }
    }
}
