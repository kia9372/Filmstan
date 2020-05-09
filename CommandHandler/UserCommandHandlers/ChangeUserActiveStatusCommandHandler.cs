﻿using Command.UserCommands;
using Common.Operation;
using MediatR;
using SiteService.Repositories.Implementation;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace CommandHandler.UserCommandHandlers
{
    public class ChangeUserActiveStatusCommandHandler : IRequestHandler<ChangeUserActiveStatusCommand, OperationResult<bool>>
    {
        private readonly IDomainUnitOfWork unitOfWork;

        public ChangeUserActiveStatusCommandHandler(IDomainUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }
        public async Task<OperationResult<bool>> Handle(ChangeUserActiveStatusCommand request, CancellationToken cancellationToken)
        {
            var getUser = await unitOfWork.UsersRepository.GetUserByIdAsync(request.Id, cancellationToken);
            if (getUser.Result != null)
            {
                getUser.Result.UserChangeActiveStatus(getUser.Result.IsActive);
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

    public class ChangeUserEmailStatusCommandHandler : IRequestHandler<ChangeUserEmailStatusCommand, OperationResult<bool>>
    {
        private readonly IDomainUnitOfWork unitOfWork;

        public ChangeUserEmailStatusCommandHandler(IDomainUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }
        public async Task<OperationResult<bool>> Handle(ChangeUserEmailStatusCommand request, CancellationToken cancellationToken)
        {
            var getUser = await unitOfWork.UsersRepository.GetUserByIdAsync(request.Id, cancellationToken);
            if (getUser.Result != null)
            {
                getUser.Result.ChangeEmailStatus(getUser.Result.ConfirmEmail);
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
