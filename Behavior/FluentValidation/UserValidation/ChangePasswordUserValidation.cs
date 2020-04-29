using DataTransfer.UserDtos;
using FluentValidation;

namespace BehaviorHandler.FlientValidation.UserValidation
{
    public class ChangePasswordUserValidation : AbstractValidator<ChangePasswordUserDto>
    {
        public ChangePasswordUserValidation()
        {
            RuleFor(x => x.Password).MinimumLength(6).NotNull().NotEmpty();
            RuleFor(x => x.ConfirmPassword).MinimumLength(6).NotNull().NotEmpty();
        }
    }
}
