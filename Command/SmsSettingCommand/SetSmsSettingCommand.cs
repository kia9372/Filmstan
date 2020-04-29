using Common.FilmStanEnums;
using Common.Operation;
using DataTransfer.SMSSettingDtos;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Command.SmsSettingCommand
{
  public  class SetSmsSettingCommand : AddSMSSetting,IRequest<OperationResult<string>>
    {
        public SetSmsSettingCommand()
        {
        }
    }
}
