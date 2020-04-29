using Domain.Aggregate.DomainAggregates.UserAggregate;
using Domain.Core.Shared;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Aggregate.DomainAggregates.RoleAggregate
{
    public class Role : Aggregates, IAggregateMarker
    {
        #region BackingFiled
        private string _name;
        private string _description;
        private Guid _securityStamp;
        #endregion
        #region Properties
        public string Name
        {
            get => _name; set => SetWithNotify(value, ref _name);
        }
        public string Description
        {
            get => _description; set => SetWithNotify(value, ref _description);
        }

        public Guid SecurityStamp
        {
            get => _securityStamp; set => SetWithNotify(value, ref _securityStamp);
        }

        public ICollection<AccessLevel> AccessLevels { get; set; }
        public ICollection<UserRole> UserRoles { get; set; }
        #endregion
        #region Ctor
        public Role(string name, string description)
        {
            SetProperties(name, description);
        }
        #endregion
        #region Set Values
        public void SetProperties(string name, string description)
        {
            Name = name ?? throw new ArgumentNullException(nameof(name));
            Description = description ?? throw new ArgumentNullException(nameof(description));
            SecurityStamp = Guid.NewGuid();
        }
        public void SetName(string name)
        {
            Name = name ?? throw new ArgumentNullException(nameof(name));
        }
        public void SetDescription(string description)
        {
            Description = description ?? throw new ArgumentNullException(nameof(description));
        }

        public void UpdateSecurityStamp()
        {
            SecurityStamp = Guid.NewGuid();
        }
        #endregion
    }
}
