using Domain.Aggregate.DomainAggregates.PostMagAggregate;
using Domain.Core.Shared;
using Sieve.Attributes;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Aggregate.DomainAggregates.CategoryAggregate
{
    public class Category : Aggregates, IAggregateMarker
    {
        #region BackingField
        private string _name;
        private Guid? _parentId;
        #endregion
        #region Properties
        [Sieve(CanFilter = true, CanSort = true)]
        public Guid? ParentId { get => _parentId; set => SetWithNotify(value, ref _parentId); }
        [Sieve(CanFilter = true, CanSort = true)]
        public string Name { get => _name; set => SetWithNotify(value, ref _name); }
        public ICollection<PostMagazine> PostMagazines { get; set; }
        #endregion
        public Category()
        {

        }
        public Category(string name, Guid? parentId)
        {
            SetProperties(name, parentId);
        }
        #region SetValues
        public void SetProperties(string name, Guid? parentId)
        {
            ParentId = parentId;
            Name = name;
        }
        #endregion
    }
}
