using System;
using System.Collections.Generic;
using System.Text;

namespace Common.StringExtentions
{
    public static class StringExtention
    {
        public static string RemoveString(this string value, string SubString)
        {
            int StartIndex = value.IndexOf(SubString);
            return value.Remove(StartIndex, SubString.Length);
        }
    }
}
