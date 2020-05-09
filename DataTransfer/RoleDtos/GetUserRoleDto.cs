using Domain.Aggregate.DomainAggregates.RoleAggregate;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataTransfer.RoleDtos
{
    public class GetUserRoleDto
    {
        public Guid CurrenrRoleId { get; set; }
        public IEnumerable<Role> Roles { get; set; }
    }
}
