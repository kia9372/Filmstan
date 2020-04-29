using DataTransfer.RoleDtos;
using FluentValidation;

namespace BehaviorHandler.FlientValidation.RoleValidation
{
    public class EditRoleValidation : AbstractValidator<EditRoleDto>
    {
        public EditRoleValidation()
        {
            RuleFor(x => x.Name).NotEmpty().NotNull();
            RuleFor(x => x.Description).NotNull().NotEmpty();
            RuleFor(x => x.Id).NotEmpty().NotNull();
        }
    }

}
