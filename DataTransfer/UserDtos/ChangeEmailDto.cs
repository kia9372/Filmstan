using System;
using System.Collections.Generic;
using System.Text;

namespace DataTransfer.UserDtos
{
    public class ChangeEmailDto
    {
        public Guid Id { get; set; }
        public string Email { get; set; }
    }
}
