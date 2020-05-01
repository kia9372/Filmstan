using Domain.Aggregate.DomainAggregates.RoleAggregate;
using Domain.Core.Shared;
using Sieve.Attributes;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Aggregate.DomainAggregates.UserAggregate
{
    public class UserRole : Aggregates, IAggregateMarker
    {
        #region BackingField
        private Guid _roleId;
        private Guid _userId;
        #endregion
        #region Properties
        [Sieve(CanFilter = true, CanSort = true)]
        public Guid RoleId { get => _roleId; private set => SetWithNotify(value, ref _roleId); }
        [Sieve(CanFilter = true, CanSort = true)]
        public Guid UserId { get => _userId; private set => SetWithNotify(value, ref _userId); }
        public User User { get; set; }
        public Role Role { get; set; }
        #endregion
        public UserRole(Guid roleId, Guid userId)
        {
            SetValues(roleId, userId);
        }

        #region SetValues
        public void SetValues(Guid roleId, Guid userId)
        {
            RoleId = roleId;
            UserId = userId;
        }

        public void ChangeRole(Guid roleId)
        {
            RoleId = roleId;
        }
        #endregion
    }
}
