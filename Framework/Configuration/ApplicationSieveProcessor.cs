using Domain.Aggregate.DomainAggregates.UserAggregate;
using Microsoft.Extensions.Options;
using Sieve.Models;
using Sieve.Services;
using System;
using System.Collections.Generic;
using System.Text;

namespace Framework.Configuration
{
    public class ApplicationSieveProcessor : SieveProcessor
    {
        public ApplicationSieveProcessor(
            IOptions<SieveOptions> options)
            : base(options)
        {
        }

        protected override SievePropertyMapper MapProperties(SievePropertyMapper mapper)
        {
            mapper.Property<User>(p => p.UserRoles.RoleId)
                .CanSort()
                .CanFilter();

            return mapper;
        }
    }
}
