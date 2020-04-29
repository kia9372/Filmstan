using AdminPanelGetWay.Domain.BaseFramework;
using Domain.Core.Shared;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;

namespace Common.FilmstanExtentions
{
    public static class ModelBuilderExtentions
    {
        public static void RegisterDbSet<BaseEntity>(this ModelBuilder builder, params Assembly[] assemblies)
        {
            IEnumerable<Type> types = assemblies.SelectMany(x => x.GetExportedTypes())
               .Where(x => x.IsPublic && !x.IsAbstract && x.IsClass && typeof(BaseEntity).IsAssignableFrom(x));
            foreach (Type type in types)
                builder.Entity(type);
        }

        public static void FilterBySoftDelete(this ModelBuilder builder)
        {
            var versionedEntities = builder.Model.GetEntityTypes()
                .Where(x => typeof(Aggregates).IsAssignableFrom(x.ClrType) && !typeof(ValueObject<>).IsAssignableFrom(x.ClrType));
            foreach (var entityType in versionedEntities)
            {
                builder.Entity(entityType.ClrType, entityBuilder =>
            {
                //Global Filters
                var lambdaExp = ApplyEntityFilterTo(entityType.ClrType);
                if (lambdaExp != null)
                    entityBuilder.HasQueryFilter(lambdaExp);
            });
            }
        }

        private static LambdaExpression ApplyEntityFilterTo(Type entityClrType)
        {
            var parameter = Expression.Parameter(entityClrType, "entity");
            var member = Expression.Property(parameter, nameof(Aggregates.IsDelete));
            var body = Expression.Equal(member, Expression.Constant(false));
            return Expression.Lambda(body, parameter);
        }
    }
}
