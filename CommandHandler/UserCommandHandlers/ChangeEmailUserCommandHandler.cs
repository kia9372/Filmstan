﻿using Command.UserCommands;
using Common.Operation;
using MediatR;
using SiteService.Repositories.Implementation;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace CommandHandler.UserCommandHandlers
{
    public class ChangeEmailUserCommandHandler : IRequestHandler<ChangeEmailUerCommand, OperationResult<bool>>
    {
        private readonly IDomainUnitOfWork unitOfWork;

        public ChangeEmailUserCommandHandler(IDomainUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }
        public async Task<OperationResult<bool>> Handle(ChangeEmailUerCommand request, CancellationToken cancellationToken)
        {
            var getUser = await unitOfWork.UsersRepository.GetUserByIdAsync(request.Id, cancellationToken);
            if (getUser.Result != null)
            {
                getUser.Result.ChangeEmail(request.Email);
                var addUser = unitOfWork.UsersRepository.Update(getUser.Result, cancellationToken);
                if (addUser.Success)
                {
                    try
                    {
                        await unitOfWork.CommitSaveChangeAsync();
                        return OperationResult<bool>.BuildSuccessResult(true);
                    }
                    catch (Exception ex)
                    {
                        return OperationResult<bool>.BuildFailure(ex.Message);
                    }
                }
            }
            return OperationResult<bool>.BuildFailure(getUser.ErrorMessage);
        }
    }
}
