using DataTransfer.Setting;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataTransfer.SMSSettingDtos
{
    public class AddSMSSetting : ISettings<AddSMSSetting>
    {
        public string LineNumber { get; set; }
        public string userApikey { get; set; }
        public string secretKey { get; set; }

        public AddSMSSetting WithDefaultValues()
        {
            return this;
        }
    }
}
