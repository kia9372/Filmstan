using Common.StringExtentions;
using DataTransfer.ControllerDtos;
using Domain.Aggregate.DomainAggregates.RoleAggregate;
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

        public static List<ControllerDto> GetLisOfController(this Assembly assembly)
        {
            List<ControllerDto> permissionList = new List<ControllerDto>();
            var ass = Assembly.GetEntryAssembly();
            foreach (var controller in Assembly.GetEntryAssembly().GetControllerList<IPermissionMarker>())
            {
                permissionList.Add(new ControllerDto
                {
                    ControllerName = controller.Name.RemoveString("Controller"),
                    ActionInfos = controller.FindActionsOfController(),
                    ControllerDisplayName = controller.GetNameByDispayAttribute()
                });
            }
            return permissionList;
        }

        public static IEnumerable<ControllerDto> FindSelectedAccess(this IEnumerable<AccessLevel> accessList)
        {

            var controllerList = Assembly.GetExecutingAssembly().GetLisOfController();
            controllerList.ForEach(controllers =>
           {
               controllers.ActionInfos.ForEach(actions =>
               {
                   foreach (var item in accessList)
                   {
                       var con = $"{controllers.ControllerName}:{actions.ActionName}";
                       if ($"{controllers.ControllerName}:{actions.ActionName}" == item.Access)
                       {
                           actions.IsSelected = true;
                       }
                   }
               });
            }
           );
            return controllerList;
        }
    }
}
