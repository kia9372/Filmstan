using Command.PostMagazinrCommands;
using Common.Operation;
using MediatR;
using SiteService.Repositories.Implementation;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace CommandHandler.PostMagazineCommandHandlers
{
    public class DeleteMagazineCommandHandler : IRequestHandler<DeletetPostMagazineCommands, OperationResult<string>>
    {
        private readonly IDomainUnitOfWork unitOfWork;

        public DeleteMagazineCommandHandler(IDomainUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }
        public async Task<OperationResult<string>> Handle(DeletetPostMagazineCommands request, CancellationToken cancellationToken)
        {
            var findPost = await unitOfWork.PostMagazineRepository.GetPostById(request.id, cancellationToken);
            if (findPost.Result != null)
            {
                findPost.Result.Delete();
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
