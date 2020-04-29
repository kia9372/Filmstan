using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Command.UserCommands;
using Common.Utilitis;
using DataTransfer.UserDtos;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Travel.Framework.Base;

namespace Filmstan.Controllers.V1.UserControllers
{
    public class RegisterController : BaseController 
    {
        private readonly IMediator mediator;

        public RegisterController(IMediator mediator) : base(mediator)
        {
            this.mediator = mediator;
        }

        [HttpPost]
        public async Task<IActionResult> AddUser([FromForm]AddUserDto userDto)
        {
            var addUser = await mediator.Send(new CreateUserCommand(userDto.Username, Utility.Hash(userDto.Password), userDto.Name, userDto.Family, userDto.PhoneNumber, userDto.Email, userDto.Photo));
            if (addUser.Success)
            {
                return Ok(addUser.Result);
            }
            return BadRequest(addUser.ErrorMessage);
        }
    }
}