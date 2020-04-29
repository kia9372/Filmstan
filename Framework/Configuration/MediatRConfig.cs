using Behavior;
using Command;
using CommandHandler;
using Event;
using EventHandler;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Query;
using QueryHandler;
using System;
using System.Collections.Generic;
using System.Text;

namespace Framework.Configuration
{
    public static class MediatRConfig
    {
        public static void ConfigMediatR(this IServiceCollection services)
        {
            var assCommand = typeof(ICommand).Assembly;
            var assCommandHandler = typeof(ICommandHandler).Assembly;
            var assQuery = typeof(IQueryScope).Assembly;
            var assQueryHandler = typeof(IQueryHandlerScop).Assembly;
            var assEvent = typeof(IEvent).Assembly;
            var assEventHandler = typeof(IEventHandler).Assembly;
            var assBehavior = typeof(IBehavior).Assembly;
            services.AddMediatR(assCommand, assCommandHandler, assQuery, assQueryHandler, assEvent, assEventHandler, assBehavior);

        }
    }
}
