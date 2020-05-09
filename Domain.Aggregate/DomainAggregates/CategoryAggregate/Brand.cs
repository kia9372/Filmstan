using Domain.Core.Shared;
using Sieve.Attributes;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Aggregate.DomainAggregates.CategoryAggregate
{
    public class Brand : Aggregates, IAggregateMarker
    {
        #region BackingField
        private string _brandName;
        private string _isoBrandName;
        private Guid _categoryId;
        #endregion
        #region Properties

        public string BrandName { get => _brandName; set => SetWithNotify(value, ref _brandName); }

        public string IsoBrandName { get => _isoBrandName; set => SetWithNotify(value, ref _isoBrandName); }

        public Guid CategoryId { get => _categoryId; set => SetWithNotify(value, ref _categoryId); }
        public Category Category { get; set; }
        #endregion
        public Brand()
        {

        }
        public Brand(string brandName, string isoBrandName, Guid categoryId)
        {
            SetValues(brandName, isoBrandName, categoryId);
        }
        #region SetValues
        public void SetValues(string brandName, string isoBrandName, Guid categoryId)
        {
            BrandName = brandName;
            IsoBrandName = isoBrandName;
            CategoryId = categoryId;
        }
        #endregion
    }
}
