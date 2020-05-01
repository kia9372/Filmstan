using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using Command.UserActivationCommands;
using Command.UserCommands;
using Common;
using Common.Utilitis;
using DataTransfer.ActivationCodeDtos;
using DataTransfer.RoleDtos;
using DataTransfer.UserDtos;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Query.UserQueries;
using Travel.Framework.Base;

namespace Command.Controllers.V1.UserControllers
{
    [DisplayName("مدیریت کاربران ")]
    public class UserController : BaseController, IPermissionMarker
    {
        private readonly IMediator mediator;

        public UserController(IMediator mediator) : base(mediator)
        {
            this.mediator = mediator;
        }



        [HttpPut]
        [DisplayName("ویرایش کاربران")]
        public async Task<IActionResult> UpdateUser([FromForm]EditUserDto userDto)
        {
            var addUser = await mediator.Send(new UpdateUserCommand(userDto.Id, userDto.Username, userDto.Name, userDto.Family, userDto.PhoneNumber, userDto.Email, ""));
            if (addUser.Success)
            {
                return Ok();
            }
            return BadRequest(addUser.ErrorMessage);
        }

        [HttpPut]
        [DisplayName("تغییر پسورد ")]
        public async Task<IActionResult> ChangePasswordUser(ChangePasswordUserDto userDto)
        {
            var addUser = await mediator.Send(new ChangePasswordUserCommand(userDto.Id, Utility.Hash(userDto.Password)));
            if (addUser.Success)
            {
                return Ok();
            }
            return BadRequest(addUser.ErrorMessage);
        }

        [HttpPost]
        public async Task<IActionResult> VerificationCode(VerificationCodeDto codeDto)
        {
            var verificationCode = await mediator.Send(new VerificationCommand { Code = codeDto.Code, HashCode = codeDto.HashCode });
            if (verificationCode.Success)
            {
                return Ok();
            }
            return BadRequest(verificationCode.ErrorMessage);
        }

        [HttpGet]
        [DisplayName("نمایش لیست کاربران به صورت صفحه بندی شده")]
        public async Task<IActionResult> GetAllUserPaging([FromQuery]GetAllFormQuery getAllRole)
        {
            var res = await mediator.Send(new GetAllUserPagingQuery
            {
                Page = getAllRole.Page,
                PageSize = getAllRole.PageSize,
                Filters = getAllRole.Filters,
                Sorts = getAllRole.Sorts
            });
            if (res.Success)
            {
                return Ok(res.Result);
            }
            return BadRequest(res.ErrorMessage);
        }
        [HttpPut]
        [DisplayName("تغییر ایمیل ")]
        public async Task<IActionResult> ChangeEmail(ChangeEmailDto emailDto)
        {
            var changeEmail = await mediator.Send(new ChangeEmailUerCommand { Email = emailDto.Email, Id = emailDto.Id });
            if (changeEmail.Success)
            {
                return Ok();
            }
            return BadRequest(changeEmail.ErrorMessage);
        }

        [HttpPut]
        [DisplayName("تغییر شماره تلفن ")]
        public async Task<IActionResult> ChangePhoneNumber(ChangePhoneNumberDto changePasswordlDto)
        {
            var changePassword = await mediator.Send(new ChangePhoneNumberUserCommand { Id = changePasswordlDto.Id, PhoneNumber = changePasswordlDto.PhoneNumber });
            if (changePassword.Success)
            {
                return Ok();
            }
            return BadRequest(changePassword.ErrorMessage);
        }

        [HttpPut]
        [DisplayName("تغییر وضعیت فعال بودن کاربران ")]
        public async Task<IActionResult> ChangeUserActiveStatus(ChangeUserActiveStatusDto changeUserActiveStatusDto)
        {
            var changeUserActiveStatus = await mediator.Send(new ChangeUserActiveStatusCommand { Id = changeUserActiveStatusDto.Id });
            if (changeUserActiveStatus.Success)
            {
                return Ok();
            }
            return BadRequest(changeUserActiveStatus.ErrorMessage);
        }

    }
}
