using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Command.LoginCommands;
using Command.UserActivationCommands;
using Command.UserCommands;
using Command.UserRoleCommands;
using Common;
using Common.HttpContextExtentions;
using Common.UploadUtility;
using Common.Utilitis;
using DataTransfer.ActivationCodeDtos;
using DataTransfer.RoleDtos;
using DataTransfer.UserDtos;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Query.RoleQueries;
using Query.UserQueries;
using Travel.Framework.Base;

namespace Command.Controllers.V1.UserControllers
{
    [DisplayName("مدیریت کاربران ")]
    [Authorize]
    public class UserController : BaseController, IPermissionMarker
    {
        private readonly IMediator mediator;
        private readonly IHttpContextAccessor httpContextAccessor;

        public UserController(IMediator mediator, IHttpContextAccessor httpContextAccessor) : base(mediator)
        {
            this.mediator = mediator;
            this.httpContextAccessor = httpContextAccessor;
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> UserInformation()
        {
            var result = await mediator.Send(new UserInformationCommand
            {
                Id = Guid.Parse(httpContextAccessor.HttpContext.User.Identity.GetUserId())
            });
            if (result.Success)
            {
                return Ok(result.Result);
            }
            return BadRequest(result.ErrorMessage);
        }

        [HttpPut]
        [DisplayName("ویرایش کاربران")]
        public async Task<IActionResult> UpdateUser([FromForm]EditUserDto userDto)
        {
            var addUser = await mediator.Send(new UpdateUserCommand(userDto.Id, userDto.Username, userDto.Name, userDto.Family, userDto.Email, userDto.Photo));
            if (addUser.Success)
            {
                return Ok();
            }
            return BadRequest(addUser.ErrorMessage);
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> GetUserById(Guid id)
        {
            var result = await mediator.Send(new GerUserByIdQuery(id));
            if (result.Success)
            {
                return Ok(result.Result);
            }
            return BadRequest(result.ErrorMessage);
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

        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> GetUserCurrentUserRole(Guid id)
        {
            var resulr = await mediator.Send(new GetUserRoleQuery { UserId = id });
            if (resulr.Success)
            {
                return Ok(resulr.Result);
            }
            return BadRequest(resulr.ErrorMessage);
        }

        [HttpPost]
        [AllowAnonymous]
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
        [DisplayName("تغییر نقش کاربر")]
        public async Task<IActionResult> ChangeUserRole(ChangeUseRoleDto useRoleDto)
        {
            var result = await mediator.Send(new UpdateUserRoleCommand(useRoleDto.RoleId, useRoleDto.UserId));
            if (result.Success)
            {
                return Ok();
            }
            return BadRequest(result.ErrorMessage);
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

        [HttpPut]
        [DisplayName("تغییر وضعیت فعال بودن ایمیل ")]
        public async Task<IActionResult> ChangeUserEmailStatus(ChangeUserEmailStatusDto changeUserActiveStatusDto)
        {
            var changeUserActiveStatus = await mediator.Send(new ChangeUserEmailStatusCommand { Id = changeUserActiveStatusDto.Id });
            if (changeUserActiveStatus.Success)
            {
                return Ok();
            }
            return BadRequest(changeUserActiveStatus.ErrorMessage);
        }

        [HttpPut]
        [DisplayName("تغییر وضعیت فعال بودن شماره تلفن ")]
        public async Task<IActionResult> ChangeUserPhoneConfirmedStatus(ChangeUserEmailStatusDto changeUserActiveStatusDto)
        {
            var changeUserActiveStatus = await mediator.Send(new ChangeUserPhoneConfirmedStatusCommand { Id = changeUserActiveStatusDto.Id });
            if (changeUserActiveStatus.Success)
            {
                return Ok();
            }
            return BadRequest(changeUserActiveStatus.ErrorMessage);
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> GetAvatar(Guid? id)
        {
            var Id = id == null ? Guid.Parse(httpContextAccessor.HttpContext.User.Identity.GetUserId()) : id;
            if (Id != null)
            {
                var user = await mediator.Send(new GerUserByIdQuery((Guid)Id));
                if (user.Success)
                {
                    var path = Path.Combine(UploadFolderPath.PathAvatarUserUploadFolder(), user.Result.Photo);
                    var memory = new MemoryStream();
                    using (var stream = new FileStream(path, FileMode.Open))
                    {
                        await stream.CopyToAsync(memory);
                    }
                    memory.Position = 0;
                    return File(memory, GetContentType(path), Path.GetFileName(path));
                }
                return BadRequest(user.ErrorMessage);
            }
            return BadRequest("Id not valid");
        }

        [HttpPut]
        [DisplayName("تغییر وضعیت بلاک بودن  کاربر ")]
        public async Task<IActionResult> ChangeUserIsLockedEndStatus(ChangeUserEmailStatusDto changeUserActiveStatusDto)
        {
            var changeUserActiveStatus = await mediator.Send(new ChangeUserLockEndStatusCommand { Id = changeUserActiveStatusDto.Id });
            if (changeUserActiveStatus.Success)
            {
                return Ok();
            }
            return BadRequest(changeUserActiveStatus.ErrorMessage);
        }

        private string GetContentType(string path)
        {
            var types = GetMimeTypes();
            var ext = Path.GetExtension(path).ToLowerInvariant();
            return types[ext];
        }

        private Dictionary<string, string> GetMimeTypes()
        {
            return new Dictionary<string, string>
            {
                {".txt", "text/plain"},
                {".pdf", "application/pdf"},
                {".doc", "application/vnd.ms-word"},
                {".docx", "application/vnd.ms-word"},
                {".xls", "application/vnd.ms-excel"},
                {".png", "image/png"},
                {".jpg", "image/jpeg"},
                {".jpeg", "image/jpeg"},
                {".gif", "image/gif"},
                {".csv", "text/csv"}
            };
        }

    }
}
