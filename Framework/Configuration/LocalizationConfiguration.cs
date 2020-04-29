using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Localization;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace Framework.Configuration
{
    public static class LocalizationConfiguration
    {
        public static void LocalizeConfiguration(this IServiceCollection services)
        {
            services.AddLocalization(opts => { opts.ResourcesPath = "Resources"; });
            services.Configure<RequestLocalizationOptions>(
        opts =>
        {
            var supportedCultures = new List<CultureInfo>
            {
                new CultureInfo("en"),
                new CultureInfo("fa")
            };
            opts.DefaultRequestCulture = new RequestCulture("fa", "fa");
            opts.SupportedCultures = supportedCultures;
            opts.SupportedUICultures = supportedCultures;

        });
        }
    }
}
