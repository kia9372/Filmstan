using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;

namespace Domain.Core.Shared
{
    public abstract class Aggregates : AggregateNotification
    {
        private bool _isDelete = false;
        public Guid Id { get; protected set; }
        public bool IsDelete { get => _isDelete; protected set => value = _isDelete; }

        public void Delete()
        {
            _isDelete = true;
        }

        public override bool Equals(object obj)
        {
            var other = obj as Aggregates;

            if (ReferenceEquals(other, null))
                return false;

            if (ReferenceEquals(this, other))
                return true;

            if (GetType() != other.GetType())
                return false;

            if (Id == Guid.Empty || other.Id == Guid.Empty)
                return false;

            return Id == other.Id;
        }

        public static bool operator ==(Aggregates a, Aggregates b)
        {
            if (ReferenceEquals(a, null) && ReferenceEquals(b, null))
                return true;

            if (ReferenceEquals(a, null) || ReferenceEquals(b, null))
                return false;

            return a.Equals(b);
        }

        public static bool operator !=(Aggregates a, Aggregates b)
        {
            return !(a == b);
        }

        public override int GetHashCode()
        {
            return (GetType().ToString() + Id).GetHashCode();
        }
    }
}
