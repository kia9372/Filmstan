using Common.SiteEnums;
using System;
using System.ComponentModel.DataAnnotations;

namespace DataTransfer.Setting
{
    public class RegisterUserSetting : ISettings<RegisterUserSetting>
    {
        public Guid RegisterRoleByAdmin { get; set; }
        public Guid RegisterRoleByUser { get; set; }
        public string? SendCodeVerificationString
        {
            get { return this.SendCodeVerifications.ToString(); }
            set
            {
                this.SendCodeVerifications= (SendCodeVerification)Enum.Parse(typeof(SendCodeVerification), value, true);
            }
        }
        [EnumDataType(typeof(SendCodeVerification))]
        public SendCodeVerification SendCodeVerifications { get; set; }

        public RegisterUserSetting WithDefaultValues()
        {
            RegisterRoleByAdmin = Guid.NewGuid();
            RegisterRoleByUser = Guid.NewGuid();
            SendCodeVerifications = SendCodeVerification.Sms;
            return this;
        }
    }
}
