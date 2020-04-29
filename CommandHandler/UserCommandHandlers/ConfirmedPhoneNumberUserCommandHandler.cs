using Command.UserCommands;
using Common.Operation;
using MediatR;
using SiteService.Repositories.Implementation;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace CommandHandler.UserCommandHandlers
{
    public class ConfirmedPhoneNumberUserCommandHandler : IRequestHandler<ConfirmedPhoneNumberUerCommand, OperationResult<bool>>
    {
        private readonly IDomainUnitOfWork unitOfWork;

        public ConfirmedPhoneNumberUserCommandHandler(IDomainUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }
        public async Task<OperationResult<bool>> Handle(ConfirmedPhoneNumberUerCommand request, CancellationToken cancellationToken)
        {
            var getUser = await unitOfWork.UsersRepository.GetUserByPhoneNumberAsync(request.PhoneNumber, cancellationToken);
            if (getUser.Result != null)
            {
                getUser.Result.ConfirmedPhoneNumber();
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
