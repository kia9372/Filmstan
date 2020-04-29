using System;
using System.Collections.Generic;
using System.Text;

namespace DataTransfer.UserInformationDtos
{
    public class UserInformationDto
    {
        public string DispayName { get; set; }
        public IEnumerable<string> AccessUnserInfos { get; set; }
    }
}
