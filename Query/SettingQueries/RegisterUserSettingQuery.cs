using Common.Operation;
using DataTransfer.Setting;
using MediatR;

namespace Query.SettingQueries
{
    public class RegisterUserSettingQuery : IRequest<OperationResult<RegisterUserSetting>>
    {
    }
}
