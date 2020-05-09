using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Command.LoginCommands;
using Command.UserCommands;
using Common.HttpContextExtentions;
using DataTransfer.LoginDtos;
using DataTransfer.SendActivationCodeDto;
using Framework.CActionResult;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using Travel.Framework.Base;

namespace Command.Controllers.V1.UserControllers
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
        [AllowAnonymous]
        public async Task<IActionResult> Login(LoginDto login)
        {
            var userLogin = await mediator.Send(new LoginCommand { Username = login.Username, Password = login.Password });
            if (userLogin.Success)
            {
                return Ok(userLogin.Result);
            }
            return new UserLoginResult(userLogin.ErrorMessage);
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> SendActivationCode(SendActiveCodeDto phoneNumber)
        {
            var result = await mediator.Send(new UserActivationcCodeRequestCommand
            {
                PhoneNumber = phoneNumber.phoneNumber
            });
            if (result.Success)
            {
                return Ok(result.Result);
            }
            return  BadRequest(result.ErrorMessage);
        }
    }
}