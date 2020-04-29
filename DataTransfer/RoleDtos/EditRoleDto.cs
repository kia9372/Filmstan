using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DataTransfer.RoleDtos
{
    public class EditRoleDto : IValidatableObject
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (Name == " " || Name == string.Empty)
                yield return new ValidationResult(
                $"the {Name} not Valid. can not be empty",
                new[] { nameof(Name) });

            if (Description == " " || Description == string.Empty)
                yield return new ValidationResult(
                $"the {Description} not Valid. can not be empty",
                new[] { nameof(Description) });

            if (Id == Guid.Empty)
                yield return new ValidationResult(
                $"the {Id} not Valid. can not be empty",
                new[] { nameof(Id) });
        }
    }
}
