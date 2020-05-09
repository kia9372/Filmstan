using Domain.Core.Shared;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Aggregate.DomainAggregates.CategoryAggregate
{
    public class CategoryProperty : Aggregates, IAggregateMarker
    {
        #region BackingField
        private string _propName;
        private CategoryTypeProp _categoryPropertyType;
        private Guid _categoryId;
        #endregion
        #region Properties
        public string PropName { get => _propName; private set => SetWithNotify(value, ref _propName); }
        public CategoryTypeProp CategoryPropertyType { get => _categoryPropertyType; private set => SetWithNotify(value, ref _categoryPropertyType); }
        public Guid CategoryId { get => _categoryId; private set => SetWithNotify(value, ref _categoryId); }
        public Category Category { get; set; }
        #endregion
        public CategoryProperty()
        {

        }
        public CategoryProperty(string propName, CategoryPropertyType categoryPropertyType, Guid categoryId)
        {
            PropName = propName ?? throw new ArgumentNullException(nameof(propName));
            CategoryPropertyType = new CategoryTypeProp(categoryPropertyType) ?? throw new ArgumentNullException(nameof(categoryPropertyType));
            CategoryId = categoryId;
            Id = Guid.NewGuid();
        }
        #region SetValue
        public void SetValues(string propName, CategoryPropertyType categoryPropertyType, Guid categoryId)
        {
            PropName = propName ?? throw new ArgumentNullException(nameof(propName));
            CategoryPropertyType = new CategoryTypeProp(categoryPropertyType) ?? throw new ArgumentNullException(nameof(categoryPropertyType));
            CategoryId = categoryId;
        }
        #endregion
    }
}
