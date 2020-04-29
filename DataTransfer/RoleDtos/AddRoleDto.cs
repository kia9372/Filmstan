using Localization.Resources.Controllers.V1.RoleControllers;
using Localization.Resources.Translations;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace DataTransfer.RoleDtos
{
    public class AddRoleDto //: IValidatableObject
    {
        public string Name { get; set; }
        public string Description { get; set; }

        //public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        //{
        //    if (Name == " " || Name == string.Empty)
        //        yield return new ValidationResult(
        //        $"the {Name} not Valid. can not be empty",
        //        new[] { nameof(Name) });

        //    if (Description == " " || Description == string.Empty)
        //        yield return new ValidationResult(
        //        $"the {Description} not Valid. can not be empty",
        //        new[] { nameof(Description) });
        //}
    }
}
