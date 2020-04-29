using System;
using System.Collections.Generic;
using System.Text;

namespace DataTransfer.Setting
{
    public interface ISettings<T> where T : ISettings<T>, new()
    {
        T WithDefaultValues();
    }
}
