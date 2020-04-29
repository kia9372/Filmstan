using AdminPanelGetWay.Domain.BaseFramework;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Domain.Aggregate.DomainAggregates.UserAggregate.ValueObjects
{
    [Owned]
    public class ActiveCode : ValueObject<ActiveCode>
    {
        [Column(nameof(Code))]
        public int Code { get; set; }

        public ActiveCode()
        {
            GenerationCode();
        }

        public void GenerationCode()
        {
            Random random = new Random();
            Code = random.Next(100000, 999999);
        }
        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Code;
        }
    }
}
