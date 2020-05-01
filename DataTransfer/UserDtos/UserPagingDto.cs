using Sieve.Attributes;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataTransfer.UserDtos
{
    public class UserPagingDto
    {
        [Sieve(CanFilter = true, CanSort = true)]
        public Guid Id { get; set; }
        [Sieve(CanFilter = true, CanSort = true)]
        public string Username { get; set; }
        [Sieve(CanFilter = true, CanSort = true)]
        public bool ConfirmEmail { get; set; }
        [Sieve(CanFilter = true, CanSort = true)]
        public string DisplayName { get; set; }
        [Sieve(CanFilter = true, CanSort = true)]
        public bool IsActive { get; set; }
        [Sieve(CanFilter = true, CanSort = true)]
        public bool IsLockedEnd { get; set; }
        [Sieve(CanFilter = true, CanSort = true)]
        public bool ConfirmPhoneNumber { get; set; }
        public UserPagingInfo UserInfos { get; set;
        }
    }

    public class UserPagingInfo
    {
        [Sieve(CanFilter = true, CanSort = true)]
        public int AccountFaile { get; set; }
        [Sieve(CanFilter = true, CanSort = true)]
        public string PhoneNumber { get; set; }
        [Sieve(CanFilter = true, CanSort = true)]
        public DateTimeOffset? LockedEnd { get; set; }
        [Sieve(CanFilter = true, CanSort = true)]
        public string? Email { get; set; }
        [Sieve(CanFilter = true, CanSort = true)]
        public string Name { get; set; }
        [Sieve(CanFilter = true, CanSort = true)]
        public string Family { get; set; }
        [Sieve(CanFilter = true, CanSort = true)]
        public string RoleName { get; set; }
        [Sieve(CanFilter = true, CanSort = true)]
        public Guid RoleId { get; set; }

    }
}
