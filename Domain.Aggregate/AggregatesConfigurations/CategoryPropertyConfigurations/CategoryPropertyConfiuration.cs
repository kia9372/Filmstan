using Domain.Aggregate.DomainAggregates.CategoryAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Aggregate.AggregatesConfigurations.CategoryPropertyConfigurations
{   
    public class CategoryPropertyConfiuration : IEntityTypeConfiguration<CategoryProperty>
    {
        public void Configure(EntityTypeBuilder<CategoryProperty> builder)
        {
            builder.Property(x => x.CategoryPropertyType).HasConversion(x => x.CategoryPropertyType, v => new CategoryTypeProp(v));
            builder.HasOne(x => x.Category).WithMany(x => x.CategoryProperties).HasForeignKey(x => x.CategoryId);
          //  builder.OwnsOne(x => x.CategoryPropertyType);
        }
    }
}
