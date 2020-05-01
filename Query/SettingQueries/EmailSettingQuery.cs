using Common.Operation;
using DataTransfer.EmailSettingDtos;
using MediatR;

namespace Query.SettingQueries
{
    public class EmailSettingQuery : IRequest<OperationResult<AddEmailSetting>>
    {
    }
}
