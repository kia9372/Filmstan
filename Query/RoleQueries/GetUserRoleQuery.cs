using Common.Operation;
using DataTransfer.RoleDtos;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Query.RoleQueries
{
    public class GetUserRoleQuery:IRequest<OperationResult<GetUserRoleDto>>
    {
        public Guid UserId { get; set; }
    }
}
