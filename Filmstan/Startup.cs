using Autofac;
using Autofac.Extensions.DependencyInjection;
using BehaviorHandler.PipeLineBehaviors.RegisterUserBehavior;
using Command.UserCommands;
using Common.Operation;
using DAL.EF.Context;
using DAL.EF.Repositories;
using DataTransfer.SettingsDto;
using Framework.Configuration;
using Framework.Filters;
using Framework.Middllwares;
using Framework.Resources;
using Framework.ResponseFormatter.ResultApi;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Localization;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;
using Serilog;
using SiteService.Repositories.Implementation;
using System.Collections.Generic;
using System.Globalization;

namespace Filmstan
{
    public class Startup
    {
        private SiteSetting siteSetting;
        public Startup(IConfiguration configuration)
        {
            siteSetting = configuration.GetSection(nameof(SiteSetting)).Get<SiteSetting>();
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }
        public ILifetimeScope AutofacContainer { get; private set; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddCors();
            services.Configure<SiteSetting>(Configuration.GetSection(nameof(SiteSetting)));
            services.LocalizeConfiguration();
            services.PublicConfgiuration();
            services.ConfigMediatR();
            services.AddSingleton((Serilog.ILogger)Log.Logger);
            services.VersionConfiguration();
            services.AddScoped<IDomainUnitOfWork, EFDomainUnitofWork>();
            services.ResultFormatterConfig();
            services.SendNotificationConfig();
            services.AddHttpContextAccessor();
            services.SqlConfiguration<FilmstanContext>(Configuration, "SqlServer");
            services.TokenAuthorize(siteSetting);
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "My API", Version = "v1" });
            });
        }

        public void ConfigureContainer(ContainerBuilder builder)
        {
            builder.PipeLineBehaviorRegister();
            builder.AutoInjectServices();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c =>
                {
                    c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API");
                });
            }
            app.UseCors(builder =>
                             builder.WithOrigins("http://localhost:4200").AllowAnyMethod().AllowAnyOrigin().AllowAnyHeader());
            app.UseMiddleware<FilmstanExceptionMiddllware>();
            this.AutofacContainer = app.ApplicationServices.GetAutofacRoot();
            app.UseHttpsRedirection();
            app.UseAuthentication();
            app.UseAuthorization();
            app.UseRouting();
            app.Localization();
            app.UseAuthorization();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
