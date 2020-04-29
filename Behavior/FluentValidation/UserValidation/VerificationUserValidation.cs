using DataTransfer.ActivationCodeDtos;
using FluentValidation;

namespace BehaviorHandler.FlientValidation.UserValidation
{
    public class VerificationUserValidation : AbstractValidator<VerificationCodeDto>
    {
        public VerificationUserValidation()
        {
            RuleFor(x => x.Code).NotEmpty().NotNull();
            RuleFor(x => x.HashCode).NotEmpty().NotNull();
        }
    }
}
