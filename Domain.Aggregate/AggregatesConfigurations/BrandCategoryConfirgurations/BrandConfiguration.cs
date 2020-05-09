using Domain.Aggregate.DomainAggregates.CategoryAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Aggregate.AggregatesConfigurations.BrandCategoryConfirgurations
{
    public class BrandConfiguration : IEntityTypeConfiguration<Brand>
    {
        public void Configure(EntityTypeBuilder<Brand> builder)
        {
            builder.HasOne(x => x.Category).WithMany(x => x.Brands).HasForeignKey(x => x.CategoryId);
        }
    }
}
