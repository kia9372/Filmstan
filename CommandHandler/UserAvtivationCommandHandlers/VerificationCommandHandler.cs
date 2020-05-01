using Command.UserActivationCommands;
using Common.Operation;
using Domain.Aggregate.DomainAggregates.UserAggregate.ValueObjects;
using MediatR;
using SiteService.Repositories.Implementation;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CommandHandler.UserAvtivationCommandHandlers
{
    public class VerificationCommandHandler : IRequestHandler<VerificationCommand, OperationResult<string>>
    {
        private readonly IDomainUnitOfWork unitOfWork;

        public VerificationCommandHandler(IDomainUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }
        public async Task<OperationResult<string>> Handle(VerificationCommand request, CancellationToken cancellationToken)
        {
            var verification = await unitOfWork.UsersRepository.ActivationCodeRepository.FindByHashCodeAndCodeAsync(request.HashCode, request.Code, cancellationToken);
            if (verification.Result != null)
            {
                if (verification.Result.DateExpire >= DateTimeOffset.UtcNow)
                {
                    var findUSer = await unitOfWork.UsersRepository.GetUserByIdAsync(verification.Result.UserId, cancellationToken);
                    if (findUSer.Result != null)
                    {
                        switch (verification.Result.CodeType.CodeTypes)
                        {
                            case CodeTypes.EmailConfirmed:
                                findUSer.Result.ConfirmedEmail();
                                break;
                            case CodeTypes.ForgetPassword:
                                break;
                            case CodeTypes.PhoneConfirmed:
                                findUSer.Result.ConfirmedPhoneNumber();
                                break;
                            case CodeTypes.RegisterCode:
                                findUSer.Result.UserChangeActiveStatus(findUSer.Result.IsActive);
                                break;
                        }
                        var reomve = unitOfWork.UsersRepository.ActivationCodeRepository.Remove(verification.Result);
                        if (reomve.Success)
                        {
                            unitOfWork.CommitSaveChange();
                            return OperationResult<string>.BuildSuccessResult("Verification Success");
                        }
                        return OperationResult<string>.BuildFailure(reomve.ErrorMessage);
                    }
                    return OperationResult<string>.BuildFailure(findUSer.ErrorMessage);
                }
                return OperationResult<string>.BuildFailure("Exiperation Time of Code");
            }
            return OperationResult<string>.BuildFailure("Code not Found");
        }
    }
}
