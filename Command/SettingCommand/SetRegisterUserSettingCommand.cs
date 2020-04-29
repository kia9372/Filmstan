using Common.FilmStanEnums;
using Common.Operation;
using DataTransfer.Setting;
using MediatR;
using System;

namespace Command.SettingCommand
{
    public class SetRegisterUserSettingCommand : RegisterUserSetting, IRequest<OperationResult<string>>
    {
        public SetRegisterUserSettingCommand()
        {
        }
    }
}
