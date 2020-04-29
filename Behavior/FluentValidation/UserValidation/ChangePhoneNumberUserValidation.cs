using DataTransfer.UserDtos;
using FluentValidation;

namespace BehaviorHandler.FlientValidation.UserValidation
{
    public class ChangePhoneNumberUserValidation : AbstractValidator<ChangePhoneNumberDto>
    {
        public ChangePhoneNumberUserValidation()
        {
            RuleFor(x => x.Id).NotEmpty().NotNull();
            RuleFor(x => x.PhoneNumber).NotNull();
        }
    }
}
