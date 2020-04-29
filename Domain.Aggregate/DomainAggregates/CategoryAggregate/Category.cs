using Domain.Aggregate.DomainAggregates.PostMagAggregate;
using Domain.Core.Shared;
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
        public Guid? ParentId { get => _parentId; set => SetWithNotify(value, ref _parentId); }
        public string Name { get => _name; set => SetWithNotify(value, ref _name); }
        public Category Categorie { get; set; }
        public HashSet<Category> Categories { get; set; }
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
