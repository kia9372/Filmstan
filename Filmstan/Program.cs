using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Resources;
using System.Threading.Tasks;
using Autofac.Extensions.DependencyInjection;
using Framework.Configuration;
using Framework.Filters;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Serilog;
using Serilog.Events;
using Serilog.Filters;
using Serilog.Formatting.Compact;
using Serilog.Sinks.MSSqlServer;

namespace Filmstan
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            Log.Logger = new LoggerConfiguration()
                     .Enrich.FromLogContext()
                     .WriteTo.Console()
                     .CreateLogger();
            try
            {
                Log.Information("Starting web host");
                await CreateHostBuilder(args).Build().RunAsync();
            }
            catch (Exception ex)
            {
                Log.Fatal(ex, "Host terminated unexpectedly");
            }
            finally
            {
                Log.CloseAndFlush();
            }
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
            .UseSerilog()
            .UseServiceProviderFactory(new AutofacServiceProviderFactory())
               .ConfigureWebHostDefaults(webBuilder =>
               {
                   webBuilder.UseStartup<Startup>();
               })
               //.UseSerilog((hostingContext, loggerConfiguration) =>
               //{
               //    loggerConfiguration
               //        .Enrich.FromLogContext();

               //    //Log requests
               //    loggerConfiguration.WriteLogRequestsToSqlServer("Data Source=.;Initial Catalog=FilmStanDB;Integrated Security=true");

               //    //Log other logs/exceptions
               //    loggerConfiguration
               //         .WriteTo.Logger(subLoggerConfiguration =>
               //         {
               //             var options = new ColumnOptions();
               //             options.Store.Remove(StandardColumn.Properties);
               //             options.Store.Add(StandardColumn.LogEvent);

               //             subLoggerConfiguration
               //                 .Filter.ByExcluding(Matching.WithProperty(nameof(LogRequestAttribute)))
               //                 .WriteTo.MSSqlServer(
               //                     connectionString: "Data Source=.;Initial Catalog=FilmStanDB;Integrated Security=true",
               //                     tableName: "RequestLogs",
               //                     restrictedToMinimumLevel: LogEventLevel.Warning,
               //                     columnOptions: options,
               //                     autoCreateSqlTable: true);
               //         });
               //})
            ;
    }
}
