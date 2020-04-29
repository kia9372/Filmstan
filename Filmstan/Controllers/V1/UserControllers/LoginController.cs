using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Command.LoginCommand;
using Common.HttpContextExtentions;
using DataTransfer.LoginDtos;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using Travel.Framework.Base;

namespace Filmstan.Controllers.V1.UserControllers
{

    public class LoginController : BaseController
    {
        private readonly IMediator mediator;
        private readonly IHttpContextAccessor httpContextAccessor;

        public LoginController(IMediator mediator, IHttpContextAccessor httpContextAccessor) : base(mediator)
        {
            this.mediator = mediator;
            this.httpContextAccessor = httpContextAccessor;
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginDto login)
        {
            var userLogin = await mediator.Send(new LoginCommand { Username = login.Username, Password = login.Password });
            if (userLogin.Success)
            {
                return Ok(userLogin.Result);
            }
            return BadRequest(userLogin.ErrorMessage);
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
    }
}