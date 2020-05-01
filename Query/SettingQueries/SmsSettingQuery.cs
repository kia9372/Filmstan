using Common.Operation;
using DataTransfer.SMSSettingDtos;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Query.SettingQueries
{
    public class SmsSettingQuery : IRequest<OperationResult<AddSMSSetting>>
    {
    }
}
