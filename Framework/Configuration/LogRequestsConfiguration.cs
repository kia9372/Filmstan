using Serilog;
using Serilog.Events;
using Serilog.Sinks.MSSqlServer;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Text;

namespace Framework.Configuration
{
    public static class LogRequestsConfigurationExtensions
    {
        public static LoggerConfiguration WriteLogRequestsToSqlServer(this LoggerConfiguration loggerConfiguration, string connectionString, string tableName = "RequestLogs")
        {
            SqlHelper.CreateDatabaseIfNotExists(connectionString);

            loggerConfiguration.WriteTo.Logger(subLoggerConfigurations =>
            {
                var options = new ColumnOptions();
                options.Store.Remove(StandardColumn.Properties);
                options.Store.Add(StandardColumn.LogEvent);
                options.LogEvent.ExcludeAdditionalProperties = true;
                options.LogEvent.ExcludeStandardColumns = true;
                //options.TimeStamp.ConvertToUtc = true;
                options.AdditionalColumns = new Collection<SqlColumn>
                {
                    new SqlColumn {ColumnName = "RequestPath", DataType = SqlDbType.NVarChar, DataLength = 500, AllowNull = false},
                    new SqlColumn {ColumnName = "RequestMethod", DataType = SqlDbType.NVarChar, DataLength = 10, AllowNull = false},
                    new SqlColumn {ColumnName = "StatusCode", DataType = SqlDbType.Int, AllowNull = false},
                    new SqlColumn {ColumnName = "Elapsed", DataType = SqlDbType.Float, AllowNull = false},
                    new SqlColumn {ColumnName = "IpAddress", DataType = SqlDbType.NVarChar, DataLength = 100, AllowNull = false},
                };

                subLoggerConfigurations
                    .Filter.FilterOnlyLogRequests()
                    //.Enrich.FromLogContext()
                    .WriteTo.MSSqlServer(
                        connectionString: connectionString,
                        tableName: tableName,
                        restrictedToMinimumLevel: LogEventLevel.Information,
                        columnOptions: options,
                        autoCreateSqlTable: true);
            });

            return loggerConfiguration;
        }
    }
}
