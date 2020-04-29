using DataTransfer.UserDtos;
using FluentValidation;

namespace BehaviorHandler.FlientValidation.UserValidation
{
    public class ChangeUserActivateUserValidation : AbstractValidator<ChangeUserActiveStatusDto>
    {
        public ChangeUserActivateUserValidation()
        {
            RuleFor(x => x.Id).NotEmpty().NotNull();
        }
    }
}
