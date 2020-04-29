using Domain.Core.Shared;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Aggregate.DomainAggregates.RoleAggregate
{
    public class AccessLevel : Aggregates, IAggregateMarker
    {
        #region Backing Field
        private Guid _roleId;
        private string _access;
        #endregion
        #region Properties
        public Guid RoleId { get => _roleId; set => SetWithNotify(value, ref _roleId); }
        public string Access { get => _access; set => SetWithNotify(value, ref _access); }
        public Role Role { get; set; }
        #endregion
        #region Ctor
        public AccessLevel()
        {

        }
        #endregion
    }
}
