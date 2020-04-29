using Domain.Core.Shared;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Domain.Aggregate.DomainAggregates.LoggAggregate
{
    public class Logs : Aggregates, IAggregateMarker
    {
        public Guid Id { get; set; }
        public string Message { get; set; }
        public string MessageTemplate { get; set; }
        public string Level { get; set; }
        public DateTimeOffset TimeStamp { get; set; }
        public string Exception { get; set; }
        [Column(TypeName = "xml")]
        public string Properties { get; set; }
        public string LogEvent { get; set; }
    }
}
