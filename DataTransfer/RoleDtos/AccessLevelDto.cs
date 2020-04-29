using System;
using System.Collections.Generic;
using System.Text;

namespace DataTransfer.RoleDtos
{
    public class AccessLevelDto
    {
        public Guid RoleId { get; set; }
        public List<string> Access { get; set; }
    }
}
