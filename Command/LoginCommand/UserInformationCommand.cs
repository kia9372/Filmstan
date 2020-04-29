using Common.Operation;
using DataTransfer.UserInformationDtos;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Command.LoginCommand
{
    public class UserInformationCommand : IRequest<OperationResult<UserInformationDto>>
    {
        public Guid Id { get; set; }
    }
}
