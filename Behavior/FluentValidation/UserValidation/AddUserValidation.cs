using DataTransfer.UserDtos;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace BehaviorHandler.FlientValidation.UserValidation
{
    public class AddUserValidation : AbstractValidator<AddUserDto>
    {
        public AddUserValidation()
        {
            RuleFor(x => x.Name).NotEmpty().NotNull();
            RuleFor(x => x.Password).MinimumLength(6).NotEmpty().NotNull();
            RuleFor(x => x.Username).NotNull().NotEmpty().MinimumLength(4);
            RuleFor(x => x.Family).NotEmpty().NotNull();
            RuleFor(x => x.Email).EmailAddress();
            RuleFor(x => x.ConfirmPassword).MinimumLength(6).NotNull().NotEmpty();
        }
    }
}
