using Command.PostMagazinrCommands;
using Common.Operation;
using Common.UploadUtility;
using MediatR;
using SiteService.Repositories.Implementation;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace CommandHandler.PostMagazineCommandHandlers
{
    public class UpdatePostMagazineCommandHandler : IRequestHandler<UpdatePostMagazineCommands, OperationResult<string>>
    {
        private readonly IDomainUnitOfWork unitOfWork;

        public UpdatePostMagazineCommandHandler(IDomainUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }
        public async Task<OperationResult<string>> Handle(UpdatePostMagazineCommands request, CancellationToken cancellationToken)
        {
            string fileName = "";
            var findPost = await unitOfWork.PostMagazineRepository.GetPostById(request.Id, cancellationToken);
            if (findPost.Result != null)
            {
                ///Upload Poster
                if (request.Photo != null)
                {
                    var uploadFile = await UploadUtiltie.UploadInCustomePath(request.Photo, ".png", request.Title, UploadFolderPath.PathPosterUploadFolder(), UploadFolderPath.PathPosterUpload());
                    if (uploadFile.Success)
                    {
                        fileName = uploadFile.Result;
                    }
                }
                /// Add New Post
                findPost.Result.SetProperties(request.Title, request.Description, request.Photo != null ? fileName : findPost.Result.Photo, request.PostContent, request.DownloadLink, request.SubTitleLink, request.CategoryId, request.WriterId);
                var add = unitOfWork.PostMagazineRepository.Update(findPost.Result, cancellationToken);
                if (add.Success)
                {
                    try
                    {
                        await unitOfWork.CommitSaveChangeAsync();
                        return OperationResult<string>.BuildSuccessResult("Add Success");
                    }
                    catch (Exception ex)
                    {
                        return OperationResult<string>.BuildSuccessResult(ex.Message);
                    }
                }
            }
            return OperationResult<string>.BuildSuccessResult(findPost.ErrorMessage);
        }
    }
}
