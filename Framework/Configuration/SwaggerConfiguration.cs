using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace Framework.Configuration
{
    public static class SwaggerConfiguration
    {
        public static void SwaggerSetup(this IServiceCollection services)
        {
            //services.AddSwaggerGen(c =>
            //{
            //    c.SwaggerDoc("v1", new OpenApiInfo
            //    {
            //        Title = "Sabacell Tallent",
            //        Version = "v1",
            //        Description = "this api for talent of Sabacell",
            //        Contact = new OpenApiContact
            //        {
            //            Name = "Kianoush Dortaj",
            //            Email = "kiadr9372@gmail.com",
            //        },
            //        License = new OpenApiLicense
            //        {
            //            Name = "Arash Karami"
            //        }
            //    });
            //});
        }
    }
}
