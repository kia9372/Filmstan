using Domain.Aggregate.DomainAggregates.RoleAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Aggregate.AggregatesConfigurations.AccessLevelConfigurations
{
    public class AccessLevelConfiguration : IEntityTypeConfiguration<AccessLevel>
    {
        public void Configure(EntityTypeBuilder<AccessLevel> builder)
        {
            builder.HasOne(x => x.Role).WithMany(x => x.AccessLevels).HasForeignKey(x => x.RoleId);
        }
    }
}
