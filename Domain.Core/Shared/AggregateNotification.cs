using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;

namespace Domain.Core.Shared
{
    public abstract class AggregateNotification : INotifyPropertyChanged, INotifyPropertyChanging
    {
        public event PropertyChangedEventHandler PropertyChanged;
        public event PropertyChangingEventHandler PropertyChanging;
        protected void SetWithNotify<T>(T value, ref T field,
        [CallerMemberName] string propertyName = "")
        {
            if (!Object.Equals(field, value))
            {
                PropertyChanging?.Invoke(this,
                new PropertyChangingEventArgs(propertyName));
                field = value; //
                PropertyChanged?.Invoke(this,
                new PropertyChangedEventArgs(propertyName));
            }
        }
    }
}
