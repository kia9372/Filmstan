﻿using System;
using System.Collections.Generic;
using System.Text;

namespace AdminPanelGetWay.DTO.Settings
{
    public class JwtSetting
    {
        public string SecretKey { get; set; }
        public string Encryptkey { get; set; }
        public string Issuer { get; set; }
        public string Audience { get; set; }
        public string ClientId { get; set; }
        public int NotBeforeMinutes { get; set; }
        public int ExpirationMinutes { get; set; }
    }
}
