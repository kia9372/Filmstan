using Command.PostMagazinrCommands;
using Common.Operation;
using Common.UploadUtility;
using Domain.Aggregate.DomainAggregates.PostMagAggregate;
using MediatR;
using SiteService.Repositories.Implementation;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CommandHandler.PostMagazineCommandHandlers
{
    public class CreatePostMagazineCommandHandler : IRequestHandler<CreatePostMagazineCommands, OperationResult<string>>
    {
        private readonly IDomainUnitOfWork unitOfWork;

        public CreatePostMagazineCommandHandler(IDomainUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }
        public async Task<OperationResult<string>> Handle(CreatePostMagazineCommands request, CancellationToken cancellationToken)
        {
            ///Upload Poster
            var uploadFile = await UploadUtiltie.UploadInCustomePath(request.Photo, ".png", request.Title, UploadFolderPath.PathPosterUploadFolder(), UploadFolderPath.PathPosterUpload());
            if (uploadFile.Success)
            {
                /// Add New Post
                PostMagazine postMagazine = new PostMagazine(request.Title, request.Description, uploadFile.Result, request.PostContent, request.DownloadLink, request.SubTitleLink, request.CategoryId, request.WriterId);
                var add = await unitOfWork.PostMagazineRepository.AddPostManagazinAsync(postMagazine, cancellationToken);
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
            return OperationResult<string>.BuildSuccessResult(uploadFile.ErrorMessage);
        }
    }
}
