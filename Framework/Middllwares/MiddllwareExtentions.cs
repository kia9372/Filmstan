using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Localization;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace Framework.Middllwares
{
    public static class MiddllwareExtentions
    {
        public static IApplicationBuilder Localization(this IApplicationBuilder builder)
        {
            var supportedCultures = new[]
  {
                    new CultureInfo("en"),
                    new CultureInfo("fr-FR"),
                    new CultureInfo("fa"),
              };
            var requestLocalizationOptions = new RequestLocalizationOptions
            {
                DefaultRequestCulture = new RequestCulture("en"),

                // Formatting numbers, dates, etc.
                SupportedCultures = supportedCultures,

                // UI strings that we have localized.
                SupportedUICultures = supportedCultures
            };
            return builder.UseRequestLocalization(requestLocalizationOptions);
        }
    }
}
