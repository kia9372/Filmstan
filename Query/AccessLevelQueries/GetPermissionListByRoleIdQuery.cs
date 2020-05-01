using Common.Operation;
using DataTransfer.ControllerDtos;
using MediatR;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace Query.AccessLevelQueries
{
    public class GetPermissionListByRoleIdQuery : IRequest<OperationResult<IEnumerable<ControllerDto>>>
    {
        public Guid RoleId { get; set; }
    }
}
