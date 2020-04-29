using DataTransfer.UserDtos;
using FluentValidation;

namespace BehaviorHandler.FlientValidation.UserValidation
{
    public class ChangeEmailUserValidation : AbstractValidator<ChangeEmailDto>
    {
        public ChangeEmailUserValidation()
        {
            RuleFor(x => x.Id).NotEmpty().NotNull();
            RuleFor(x => x.Email).EmailAddress().NotNull();
        }
    }
}
