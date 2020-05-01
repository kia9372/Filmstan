using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using Command.AccessLevelCommands;
using Common;
using Common.FilmstanExtentions;
using Common.HttpContextExtentions;
using Common.StringExtentions;
using DataTransfer.ControllerDtos;
using DataTransfer.RoleDtos;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Query.AccessLevelQueries;
using Travel.Framework.Base;

namespace Command.Controllers.V1.RoleControllers
{
    [Authorize]
    [DisplayName("تعییت سطح دسترسی")]
    public class AccessLevelController : BaseController, IPermissionMarker
    {
        private readonly IMediator mediator;
        private readonly IHttpContextAccessor contextAccessor;

        public AccessLevelController(IMediator mediator, IHttpContextAccessor contextAccessor) : base(mediator)
        {
            this.mediator = mediator;
            this.contextAccessor = contextAccessor;
        }

        [HttpGet]
        [DisplayName("لیست  سطوح دسترسی")]
        [Route("{roleId}")]
        public async Task<IEnumerable<ControllerDto>> GetPermissionList(Guid roleId)
        {
            var result = await mediator.Send(new GetPermissionListByRoleIdQuery
            {
                RoleId = roleId
            });

            return result.Result;
        }

        [HttpPost]
        //  [DisplayName("ویرایش سطح دسترسی نقش ها   ")]
        public async Task<IActionResult> SetAcceessLevel(AccessLevelDto accessLevels)
        {
            var result =await mediator.Send(new SetAccessLevelCommand
            (
                 accessLevels.RoleId,
                 accessLevels.Access
            ));
            return Ok();
        }
    }
}