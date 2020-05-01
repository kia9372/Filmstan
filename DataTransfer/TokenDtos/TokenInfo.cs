using System;
using System.Collections.Generic;
using System.Text;

namespace DataTransfer.TokenDtos
{
    public class TokenInfo
    {
        public string UserName { get; set; }
        public Guid UserSecurityStamp { get; set; }
        public Guid RoleSecurityStamp { get; set; }
        public Guid RoleId { get; set; }
        public Guid UserId { get; set; }
        public bool IsActive { get; set; }
        public List<string> AccessLevels { get; set; }
    }
}
