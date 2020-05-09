using Command.UserCommands;
using Common.Operation;
using Common.UploadUtility;
using Domain.Aggregate.DomainAggregates.UserAggregate;
using MediatR;
using SiteService.Repositories.Implementation;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace CommandHandler.UserCommandHandlers
{
    public class UpdateCommandHandler : IRequestHandler<UpdateUserCommand, OperationResult<bool>>
    {
        private readonly IDomainUnitOfWork unitOfWork;

        public UpdateCommandHandler(IDomainUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }
        public async Task<OperationResult<bool>> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
        {
            string fileName = null;
            var getUser = await unitOfWork.UsersRepository.GetUserByIdAsync(request.Id, cancellationToken);
            if (getUser.Result != null)
            {
                if (request.Photo != null)
                {
                    var uploadFile = await UploadUtiltie.UploadInCustomePath(request.Photo, ".png", request.Username, UploadFolderPath.PathUserUploadFolder(), UploadFolderPath.PathAvatarUserUploadFolder());
                    fileName = uploadFile.Result;
                }
                getUser.Result.UpdateProperties(request.Username, request.Name, request.Family, request.Email,fileName);
                var addUser = unitOfWork.UsersRepository.Update(getUser.Result, cancellationToken);
                if (addUser.Success)
                {
                    try
                    {
                        unitOfWork.CommitSaveChange();
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
