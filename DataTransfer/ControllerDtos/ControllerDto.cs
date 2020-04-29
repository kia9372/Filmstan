using System;
using System.Collections.Generic;
using System.Text;

namespace DataTransfer.ControllerDtos
{
    public class ControllerDto
    {
        public string ControllerName { get; set; }
        public string ControllerDisplayName { get; set; }
        public List<ActionDto> ActionInfos { get; set; }
    }
}
