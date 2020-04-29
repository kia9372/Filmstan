using Command.CategoryCommands;
using Common.Operation;
using MediatR;
using SiteService.Repositories.Implementation;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace CommandHandler.CategoryCommandHandlers
{
    public class DeleteCategoryCommandHandler : IRequestHandler<DeleteCategoryCommand, OperationResult<string>>
    {
        private readonly IDomainUnitOfWork unitOfWork;

        public DeleteCategoryCommandHandler(IDomainUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }
        public async Task<OperationResult<string>> Handle(DeleteCategoryCommand request, CancellationToken cancellationToken)
        {
            try
            {

                var getCategory = await unitOfWork.CategoryRepository.GetCategoryByIdAsync(request.Id, cancellationToken);
                if (getCategory.Result != null)
                {
                    getCategory.Result.Delete();
                    var updateCategory = unitOfWork.CategoryRepository.UpdateCategory(getCategory.Result, cancellationToken);
                    if (updateCategory.Success)
                    {
                         unitOfWork.CommitSaveChange();
                        return OperationResult<string>.BuildSuccessResult(updateCategory.Result);
                    }
                }
                return OperationResult<string>.BuildFailure(getCategory.ErrorMessage);
            }
            catch (Exception ex)
            {
                return OperationResult<string>.BuildFailure(ex.Message);
            }
        }
    }
}

