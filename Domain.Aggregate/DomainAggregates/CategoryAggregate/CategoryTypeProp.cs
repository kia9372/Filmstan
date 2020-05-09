using AdminPanelGetWay.Domain.BaseFramework;
using Domain.Core.Shared;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Domain.Aggregate.DomainAggregates.CategoryAggregate
{
    public class CategoryTypeProp : ValueObject<CategoryTypeProp>
    {
        [Column("CategoryPropertyType")]
        public CategoryPropertyType CategoryPropertyType { get; set; }

        public CategoryTypeProp(CategoryPropertyType CategoryPropertyType)
        {
            this.CategoryPropertyType = CategoryPropertyType;
        }
        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return CategoryPropertyType;
        }
    }
}
