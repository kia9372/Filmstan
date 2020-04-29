
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using System.Text.RegularExpressions;

namespace DataTransfer.UserDtos
{
    public class AddUserDto : IValidatableObject
    {
        public string Username { get; set; }
        public string Name { get; set; }
        public string Family { get; set; }
        public string PhoneNumber { get; set; }
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }
        public string? Email { get; set; }
        public IFormFile? Photo { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (Password != ConfirmPassword)
                yield return new ValidationResult(
                $"the Password and ConfirmPassword not equal",
                new[] { nameof(Password) });
        }
    }
}
