using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace Framework.Configuration
{
    public static class SQLDBConfiguration
    {
        public static void SqlConfiguration<DBContext>(
              this IServiceCollection services
            , IConfiguration configuration
            , string connectionStringName) where DBContext : DbContext
        {
            services.AddDbContext<DBContext>(options =>
            {
                options.UseSqlServer(configuration.GetConnectionString(connectionStringName));
            });
        }
    }
}
