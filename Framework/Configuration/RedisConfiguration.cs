using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace Framework.Configuration
{
    public static class RedisConfiguration
    {
        public static void RedisConfig(this IServiceCollection services)
        {
            services.AddDistributedRedisCache(option =>
            {
                option.Configuration = "localhost:6379";
                option.InstanceName = "";
            });
        }

    }
}
