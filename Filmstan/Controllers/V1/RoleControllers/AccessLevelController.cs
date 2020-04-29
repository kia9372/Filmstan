using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using Command.AccessLevelCommands;
using Common.FilmstanExtentions;
using Common.StringExtentions;
using DataTransfer.ControllerDtos;
using DataTransfer.RoleDtos;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Travel.Framework.Base;

namespace Filmstan.Controllers.V1.RoleControllers
{
    [DisplayName("تعییت سطح دسترسی")]
    public class AccessLevelController : BaseController, IPermissionMarker
    {
        private readonly IMediator mediator;

        public AccessLevelController(IMediator mediator) : base(mediator)
        {
            this.mediator = mediator;
        }

        [HttpGet]
        [DisplayName("لیست  سطوح دسترسی")]
        public List<ControllerDto> GetPermissionList()
        {
            List<ControllerDto> permissionList = new List<ControllerDto>();
            foreach (var controller in typeof(Startup).Assembly.GetControllerList<IPermissionMarker>())
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

        [HttpPost]
        [DisplayName("ویرایش سطح دسترسی نقش ها   ")]
        public IActionResult SetAcceessLevel(AccessLevelDto accessLevels)
        {
            var result = mediator.Send(new SetAccessLevelCommand
            (
                 accessLevels.RoleId,
                 accessLevels.Access
            ));
            return Ok();
        }
    }
}