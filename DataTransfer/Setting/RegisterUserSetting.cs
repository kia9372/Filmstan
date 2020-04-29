using Common.SiteEnums;
using System;

namespace DataTransfer.Setting
{
    public class RegisterUserSetting : ISettings<RegisterUserSetting>
    {
        public Guid RegisterRoleByAdmin { get; set; }
        public Guid RegisterRoleByUser { get; set; }
        public SendCodeVerification SendCodeVerification { get; set; }

        public RegisterUserSetting WithDefaultValues()
        {
            RegisterRoleByAdmin = Guid.NewGuid();
            RegisterRoleByUser = Guid.NewGuid();
            SendCodeVerification = SendCodeVerification.Sms;
            return this;
        }
    }
}
