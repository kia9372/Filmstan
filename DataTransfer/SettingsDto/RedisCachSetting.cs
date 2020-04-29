using System;
using System.Collections.Generic;
using System.Text;

namespace AdminPanelGetWay.DTO.Settings
{
   public class RedisCachSetting
    {
        public bool Enabled { get; set; }
        public string ConnectionString { get; set; }
    }
}
