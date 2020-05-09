using Common.FilmstanExtentions;
using Domain.Aggregate;
using Domain.Core.Shared;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace DAL.EF.Context
{
    public class FilmstanContext : DbContext
    {
        public FilmstanContext()
        {
        }

        public FilmstanContext(DbContextOptions options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.RegisterDbSet<IAggregateMarker>(typeof(IAggregateMarker).Assembly);
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(IAggregateMarker).Assembly);
            modelBuilder.FilterBySoftDelete();
            //modelBuilder.HasChangeTrackingStrategy(ChangeTrackingStrategy.ChangingAndChangedNotifications);
            base.OnModelCreating(modelBuilder);
        }

  
    }
}
