using Domain.Aggregate.DomainAggregates.UserAggregate;
using Domain.Aggregate.DomainAggregates.UserAggregate.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Aggregate.AggregatesConfigurations.ActivationCodeConfigurations
{
    public class ActivationCodeConfiguration : IEntityTypeConfiguration<ActivationCode>
    {
        public void Configure(EntityTypeBuilder<ActivationCode> builder)
        {
            builder.HasOne(x => x.User).WithMany(x => x.ActivationCodes).HasForeignKey(x => x.UserId);
            builder.OwnsOne(x => x.ActivateCode);
            builder.OwnsOne(x => x.CodeType);

        }
    }
}
