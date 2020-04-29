using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;

namespace Common.FilmstanExtentions
{
    public static class EnumExtentions
    {
        public static string EnumToDisplayName(this Enum value, DisplayProperty property = DisplayProperty.Name)
        {
            var attribute = value.GetType().GetField(value.ToString())
                .GetCustomAttributes<DisplayAttribute>(false).FirstOrDefault();
            if (attribute == null)
                return value.ToString();
            var propValue = attribute.GetType().GetProperty(property.ToString()).GetValue(attribute, null);
            return propValue.ToString();
        }

        public static string EnumToString(this Enum value)
        {
            return value.ToString();
        }

    }
    public enum DisplayProperty
    {
        Description,
        GroupName,
        Name,
        Prompt,
        ShortName,
        Order
    }
}
