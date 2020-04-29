using DataTransfer.Setting;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using System.Text.Json.Serialization;
using System.Text.RegularExpressions;

namespace DataTransfer.EmailSettingDtos
{
    public class AddEmailSetting : IValidatableObject, ISettings<AddEmailSetting>
    {
        [JsonPropertyName("From")]
        public string From { get; set; }
        [JsonPropertyName("SmtpServer")]
        public string SmtpServer { get; set; }
        [JsonPropertyName("Port")]
        public int Port { get; set; }
        [JsonPropertyName("Username")]
        public string Username { get; set; }
        [JsonPropertyName("Password")]
        public string Password { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (!Regex.IsMatch(From, @"\A(?:[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?)\Z", RegexOptions.IgnoreCase))
            {
                yield return new ValidationResult(
                $"the {From} not Valid.",
                new[] { nameof(From) });
            }
        }

        public AddEmailSetting WithDefaultValues()
        {
            return this;
        }
    }
}
