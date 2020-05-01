using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using Command.RoleCommands;
using Common;
using DataTransfer;
using DataTransfer.RoleDtos;
using Domain.Aggregate.DomainAggregates.RoleAggregate;
using Framework.Filters;
using Framework.ResponseFormatter.ResultApi;
using Localization.Resources.Controllers.V1.RoleControllers;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using Query.RoleQueries;
using Sieve.Models;
using Sieve.Services;
using Travel.Framework.Base;


namespace Command.Controllers.V1.RoleControllers
{

    [DisplayName("مدیریت نقش ها")]
    public class RoleController : BaseController, IPermissionMarker
    {
        private readonly IMediator mediator;
        private readonly ISieveProcessor sieveProcessor;
        private readonly IStringLocalizer<RoleControllerShared> _localizer;

        public RoleController(IMediator mediator, ISieveProcessor sieveProcessor, IStringLocalizer<RoleControllerShared> localizer) : base(mediator)
        {
            this.mediator = mediator;
            this.sieveProcessor = sieveProcessor;
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
        public async Task<IActionResult> GetAllRoles()
        {
            var getAll = await mediator.Send(new GetAllRoleQuery());
            if (getAll.Success)
            {
                return Ok(getAll.Result);
            }
            return BadRequest(getAll.ErrorMessage);
        }

        [HttpGet]
        [DisplayName("لیست نقش ها به صورت صفحه بندی")]
        public async Task<IActionResult> GetRoles([FromQuery]GetAllFormQuery getAllRole)
        {
            try
            {
                var res = await mediator.Send(new GetRoleListPagingQuery { Sorts = getAllRole.Sorts, Page = getAllRole.Page, Filters = getAllRole.Filters, PageSize = getAllRole.PageSize });
                if (res.Success)
                {
                    return Ok(res.Result);
                }
                return BadRequest(res.ErrorMessage);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }
    }
}
