using Domain.Aggregate.DomainAggregates.PostMagAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Aggregate.AggregatesConfigurations.PostManagazineConfigurations
{
    public class PostManagazineConfiguration : IEntityTypeConfiguration<PostMagazine>
    {
        public void Configure(EntityTypeBuilder<PostMagazine> builder)
        {
            builder.HasOne(x => x.Category).WithMany(x => x.PostMagazines).HasForeignKey(x => x.CategoryId);
            builder.HasOne(x => x.User).WithMany(x => x.PostMagazines).HasForeignKey(x => x.WriterId);
        }
    }
}
