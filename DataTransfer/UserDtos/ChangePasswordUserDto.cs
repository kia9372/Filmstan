using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DataTransfer.UserDtos
{
    public class ChangePasswordUserDto : IValidatableObject
    {
        public Guid Id { get; set; }
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (Password != ConfirmPassword)
                yield return new ValidationResult(
                $"the Password and ConfirmPassword not equal",
                new[] { nameof(Password) });
        }
    }
}
