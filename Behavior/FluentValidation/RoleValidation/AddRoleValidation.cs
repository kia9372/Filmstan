using DataTransfer.RoleDtos;
using FluentValidation;
using Localization.Resources.Translations;
using Microsoft.Extensions.Localization;

namespace BehaviorHandler.FlientValidation.RoleValidation
{
    public class AddRoleValidation : AbstractValidator<AddRoleDto>
    {
        public AddRoleValidation(IStringLocalizer<ValidationTranslate> localizer)
        {
            RuleFor(x => x.Name).NotEmpty().NotNull();
            RuleFor(x => x.Description).NotEmpty().NotNull();
        }
    }
}
