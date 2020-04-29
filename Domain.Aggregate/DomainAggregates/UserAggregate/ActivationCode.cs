using Domain.Aggregate.DomainAggregates.UserAggregate.ValueObjects;
using Domain.Core.Shared;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Aggregate.DomainAggregates.UserAggregate
{
    public class ActivationCode : Aggregates, IAggregateMarker
    {
        #region Backing Filed
        private ActiveCode _activateCode;
        private Guid _userId;
        private CodeType _codeType;
        public string _hashCode;
        public DateTimeOffset _dateExpire;
        #endregion
        #region Properties
        public ActiveCode ActivateCode { get => _activateCode; set => SetWithNotify(value, ref _activateCode); }
        public Guid UserId { get => _userId; set => SetWithNotify(value, ref _userId); }
        public CodeType CodeType { get => _codeType; set => SetWithNotify(value, ref _codeType); }
        public string HashCode { get => _hashCode; set => SetWithNotify(value, ref _hashCode); }
        public DateTimeOffset DateExpire { get => _dateExpire; set => SetWithNotify(value, ref _dateExpire); }
        public User User { get; set; }
        #endregion
        public ActivationCode()
        {

        }
        public ActivationCode(Guid userId, CodeTypes type, string hashCode)
        {
            SetProperties(userId, type, hashCode);
        }
        #region SetValues
        public void SetProperties(Guid userId, CodeTypes type, string hashCode)
        {
            ActivateCode = new ActiveCode();
            UserId = userId;
            CodeType = new CodeType(type);
            HashCode = hashCode;
            DateExpire = DateTimeOffset.UtcNow.AddMinutes(2);
        }
        #endregion
    }
}
