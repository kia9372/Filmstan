using AdminPanelGetWay.DTO.Settings;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataTransfer.SettingsDto
{
    public class SiteSetting
    {
        public JwtSetting JwtSetting { get; set; }
        public RedisCachSetting RedisCachSetting { get; set; }
    }
}
