using DataTransfer.ControllerDtos;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Text;

namespace Common.FilmstanExtentions
{
    public static class ControllerActionExtentions
    {
        public static List<Type> GetControllerList<BaseType>(this Assembly assembly)
        {
            return assembly.GetExportedTypes()
                                        .Where(t => typeof(BaseType).IsAssignableFrom(t))
                                         .Where(x => x != null && x.IsClass && !x.IsInterface && !x.IsAbstract)
                                          .ToList();
        }

        public static string GetNameByDispayAttribute(this Type type)
        {
            return type.GetCustomAttributes(typeof(DisplayNameAttribute), true).Cast<DisplayNameAttribute>().SingleOrDefault().DisplayName;
        }

        public static string GetNameByDispayAttribute(this MethodInfo type)
        {
            return type.GetCustomAttributes(typeof(DisplayNameAttribute), true).Cast<DisplayNameAttribute>().SingleOrDefault().DisplayName;
        }

        public static List<ActionDto> FindActionsOfController(this Type type)
        {
            var actionInfoList = new List<ActionDto>();
            var actions = type.GetMethods(BindingFlags.Instance | BindingFlags.DeclaredOnly | BindingFlags.Public);

            foreach (var action in actions)
            {
                string actionPermissionName = action.GetCustomAttributes<DisplayNameAttribute>()
                                                              .Select(t => t.DisplayName).FirstOrDefault();
                if (actionPermissionName != null)
                {
                    actionInfoList.Add(new ActionDto
                    {
                        ActionDisplayName = actionPermissionName,
                        ActionName = action.Name
                    });
                }
            }
            return actionInfoList;
        }
    }
}
