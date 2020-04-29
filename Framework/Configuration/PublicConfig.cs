using Behavior;
using FluentValidation.AspNetCore;
using Framework.Filters;
using Localization.Resources.Controllers.V1.RoleControllers;
using Localization.Resources.Translations;
using Microsoft.AspNetCore.Mvc.Razor;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace Framework.Configuration
{
    public static class PublicConfig
    {
        public static void PublicConfgiuration(this IServiceCollection services)
        {
            services.AddControllers(startupOption =>
            {
                startupOption.ReturnHttpNotAcceptable = true;
                startupOption.Filters.Add<ResultApi>();
            })
                .AddDataAnnotationsLocalization(options =>
                {
                    options.DataAnnotationLocalizerProvider = (type, factory) =>
                    {
                        var assemblyName = new AssemblyName(typeof(ValidationTranslate).GetTypeInfo().Assembly.FullName);
                        var cat= factory.Create("Translations.ValidationTranslate", assemblyName.Name);
                        return cat;
                    };
                })
                .AddViewLocalization(LanguageViewLocationExpanderFormat.Suffix)
                .AddFluentValidation(fvc => fvc.RegisterValidatorsFromAssemblyContaining<IBehavior>())
                .AddNewtonsoftJson(options =>
                 options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore
             );
        }
    }
}
