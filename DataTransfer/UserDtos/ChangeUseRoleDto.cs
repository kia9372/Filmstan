using System;
using System.Collections.Generic;
using System.Text;

namespace DataTransfer.UserDtos
{
    public class ChangeUseRoleDto
    {
        public Guid UserId { get; set; }
        public Guid RoleId { get; set; }
    }
}
