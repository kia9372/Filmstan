using System;
using System.Collections.Generic;
using System.Text;

namespace DataTransfer.ActivationCodeDtos
{
    public class VerificationCodeDto
    {
        public string HashCode { get; set; }
        public int Code { get; set; }
    }
}
