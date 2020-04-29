using Framework.Filters;
using Framework.ResponseFormatter.ResultApi;
using Framework.SendNotificationStrategy;
using Microsoft.Extensions.DependencyInjection;
using SiteService.SendNotificationStrategy;
using System;
using System.Collections.Generic;
using System.Text;

namespace Framework.Configuration
{
    public static class ResultForamtterConfig
    {
        public static void ResultFormatterConfig(this IServiceCollection services)
        {
            services.AddScoped<ResposeFormat, OkResultFormatter>();
            services.AddScoped<ResposeFormat, BadRequestResultFormatter>();
            services.AddScoped<ResposeFormat, OkObjectResultFormatter>();
            services.AddScoped<ResposeFormat, BadRquestObjectresultFormatter>();
            services.AddScoped<ResposeFormat, ObjectResultFormatter>();
        }

        public static void SendNotificationConfig(this IServiceCollection services)
        {
            services.AddScoped<SendNotif, SendNotifByEmail>();
            services.AddScoped<SendNotif, SendNotifBySms>();
            services.AddScoped<SendNotif, SendNotifByBoth>();
        }
    }
}
