using DataTransfer.UserDtos;
using FluentValidation;

namespace BehaviorHandler.FlientValidation.UserValidation
{
    public class EditUserValidation : AbstractValidator<EditUserDto>
    {
        public EditUserValidation()
        {
            RuleFor(x => x.Id).NotEmpty().NotNull();
            RuleFor(x => x.Name).NotEmpty().NotNull();
            RuleFor(x => x.Username).NotNull().NotEmpty().MinimumLength(4);
            RuleFor(x => x.Family).NotEmpty().NotNull();
            RuleFor(x => x.Email).EmailAddress();
        }
    }
}
