using Command.LoginCommand;
using Common.Operation;
using DataTransfer.UserInformationDtos;
using MediatR;
using SiteService.Repositories.Implementation;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CommandHandler.LoginCommadnHandlers
{
    public class UserInformationCommandHandler : IRequestHandler<UserInformationCommand, OperationResult<UserInformationDto>>
    {
        private readonly IDomainUnitOfWork unitOfWork;

        public UserInformationCommandHandler(IDomainUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }
        public async Task<OperationResult<UserInformationDto>> Handle(UserInformationCommand request, CancellationToken cancellationToken)
        {
            var information = await unitOfWork.UsersRepository.GetUserInformation(request.Id);
            if(information.Success)
            {
                return OperationResult<UserInformationDto>.BuildSuccessResult(information.Result);
            }
            return OperationResult<UserInformationDto>.BuildFailure(information.ErrorMessage);
        }
    }
}
