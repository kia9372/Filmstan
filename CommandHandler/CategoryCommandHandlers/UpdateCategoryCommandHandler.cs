using Command.CategoryCommands;
using Common.Operation;
using MediatR;
using SiteService.Repositories.Implementation;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace CommandHandler.CategoryCommandHandlers
{
    public class UpdateCategoryCommandHandler : IRequestHandler<UpdateCategoryCommand, OperationResult<string>>
    {
        private readonly IDomainUnitOfWork unitOfWork;

        public UpdateCategoryCommandHandler(IDomainUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }
        public async Task<OperationResult<string>> Handle(UpdateCategoryCommand request, CancellationToken cancellationToken)
        {
            try
            {

                var getCategory = await unitOfWork.CategoryRepository.GetCategoryByIdAsync(request.Id, cancellationToken);
                if (getCategory.Result != null)
                {
                    getCategory.Result.SetProperties(request.Name, request.ParentId);
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

