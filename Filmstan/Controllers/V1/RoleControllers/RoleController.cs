using System;
using System.ComponentModel;
using System.Threading.Tasks;
using Command.RoleCommands;
using DataTransfer.RoleDtos;
using Framework.Filters;
using Framework.ResponseFormatter.ResultApi;
using Localization.Resources.Controllers.V1.RoleControllers;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using Query.RoleQueries;
using Travel.Framework.Base;


namespace Filmstan.Controllers.V1.RoleControllers
{
    [Authorize]
    [DisplayName("مدیریت نقش ها")]
    public class RoleController : BaseController , IPermissionMarker
    {
        private readonly IMediator mediator;
        private readonly IStringLocalizer<RoleControllerShared> _localizer;

        public RoleController(IMediator mediator, IStringLocalizer<RoleControllerShared> localizer) : base(mediator)
        {
            this.mediator = mediator;
            this._localizer = localizer;
        }

        [HttpPost]
    //   [AuthorizeAccess("")]
        [DisplayName("ایجاد نقش جدید")]
        public async Task<IActionResult> AddRole(AddRoleDto addRole)
        {
            try
            {
                var add = await mediator.Send(new CreateRoleCommand(addRole.Name, addRole.Description));
                if (add.Success)
                {
                    return Ok(_localizer[RoleControllerShared.AddSuccessRole].Value);
                }
                return BadRequest(add.ErrorMessage);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [HttpPut]
        [DisplayName("ویرایش نقش ")]
        public async Task<ReturnResult> UpdateRole(EditRoleDto addRole)
        {
            try
            {
                var add = await mediator.Send(new UpdateRoleCommand(addRole.Name, addRole.Description, addRole.Id));
                if (add.Success)
                {
                    return Ok();
                }
                return BadRequest(add.ErrorMessage);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [HttpDelete]
        [Route("{id}")]
        [DisplayName("حذف نقش ")]
        public async Task<ReturnResult> DeleteRole(Guid id)
        {
            try
            {
                var add = await mediator.Send(new DeleteRoleCommand(id));
                if (add.Success)
                {
                    return Ok();
                }
                return BadRequest(add.ErrorMessage);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> GetRole(Guid id)
        {
            try
            {
                var add = await mediator.Send(new GetRoleByIdQuery(id));
                if (add.Success)
                {
                    return Ok(add.Result);
                }
                return BadRequest(add.ErrorMessage);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [HttpGet]
        [DisplayName("لیست نقش ها")]
        public async Task<IActionResult> GetRoles()
        {
            try
            {
                var add = await mediator.Send(new GetRoleListQuery());
                if (add.Success)
                {
                    return Ok(add.Result);
                }
                return BadRequest(add.ErrorMessage);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }
    }
}
