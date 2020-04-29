using Common.FilmStanEnums;
using Common.Operation;
using DataTransfer.EmailSettingDtos;
using DataTransfer.Setting;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Command.SettingCommand
{
    public class CreateEmailSettingCommand : AddEmailSetting, IRequest<OperationResult<string>>
    {
        public CreateEmailSettingCommand()
        {
        }
    }
}
