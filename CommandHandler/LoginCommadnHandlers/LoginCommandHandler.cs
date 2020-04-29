using Command.LoginCommand;
using CommandHandler.LoginCommadnHandlers.ValidateUserLogin;
using Common.Operation;
using Common.Utilitis;
using MediatR;
using SiteService.Repositories.Implementation;
using SiteService.Services.Contract;
using System.Threading;
using System.Threading.Tasks;

namespace CommandHandler.LoginCommadnHandlers
{
    public class LoginCommandHandler : IRequestHandler<LoginCommand, OperationResult<string>>
    {
        private readonly IDomainUnitOfWork unitOfWork;
        private readonly ITokenService tokenService;

        public LoginCommandHandler(IDomainUnitOfWork unitOfWork , ITokenService tokenService)
        {
            this.unitOfWork = unitOfWork;
            this.tokenService = tokenService;
        }
        public async Task<OperationResult<string>> Handle(LoginCommand request, CancellationToken cancellationToken)
        {
            var findUSer = await unitOfWork.UsersRepository.GetUserByUsernameAsync(request.Username, cancellationToken);
            if (findUSer.Result != null)
            {
                if (Utility.VerifyHashedPassword(findUSer.Result.Password, request.Password))
                {
                    var validate = new CheckIsActive(new CheckPhoneConfirmed(new CheckIsLockedAccount(null)));
                    var validateUserContext = validate.ValidateUserLogin(new UserContext
                    {
                        Context = findUSer.Result,
                        Message = null
                    });
                    if (validateUserContext.Message == null)
                    {
                        var findUserInfo = await unitOfWork.UsersRepository.GetUserTokenInfo(findUSer.Result.Id);
                    var token=   await tokenService.GenerateToken(findUserInfo.Result);
                        return OperationResult<string>.BuildSuccessResult(token);
                    }
                    return OperationResult<string>.BuildFailure(validateUserContext.Message);
                }
                else
                {
                    findUSer.Result.AccountFailed();
                    try
                    {
                        unitOfWork.CommitSaveChange();
                    }
                    catch (System.Exception ex)
                    {
                        return OperationResult<string>.BuildFailure(ex.Message);
                    }
                    return OperationResult<string>.BuildFailure("Username or Password not matched");
                }
            }
            return OperationResult<string>.BuildFailure("not Found User");
        }
    }
}
