using AdminPanelGetWay.Domain.BaseFramework;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Domain.Aggregate.DomainAggregates.UserAggregate.ValueObjects
{
    public class CodeType : ValueObject<CodeType>
    {
        [Column(nameof(CodeTypes))]
        public CodeTypes CodeTypes { get; set; }

        public CodeType()
        {

        }
        public CodeType(CodeTypes typeCode)
        {
            CodeTypes = typeCode;
        }
        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return CodeTypes;
        }
    }
}
