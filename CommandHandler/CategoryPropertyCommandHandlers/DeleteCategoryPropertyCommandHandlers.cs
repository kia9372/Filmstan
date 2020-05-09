using Command.CategoryPropertyCommands;
using Common.Operation;
using Domain.Aggregate.DomainAggregates.CategoryAggregate;
using MediatR;
using SiteService.Repositories.Implementation;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace CommandHandler.CategoryPropertyCommandHandlers
{
    public class DeleteCategoryPropertyCommandHandlers : IRequestHandler<DeleteCategoryPropertyCommand, OperationResult<string>>
    {
        private readonly IDomainUnitOfWork unitOfWork;

        public DeleteCategoryPropertyCommandHandlers(IDomainUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }
        public async Task<OperationResult<string>> Handle(DeleteCategoryPropertyCommand request, CancellationToken cancellationToken)
        {
            var deleteCategoryProperty = await unitOfWork.CategoryRepository.CategoryPropertyRepository.GetCategoryPropertybyCategoryId(request.CategoryPropertyId, cancellationToken);
            if (deleteCategoryProperty.Success)
            {
                deleteCategoryProperty.Result.Delete();
                var update = unitOfWork.CategoryRepository.CategoryPropertyRepository.UpdateCategoryProperty(deleteCategoryProperty.Result, cancellationToken);
                if (update.Success)
                {
                    await unitOfWork.CommitSaveChangeAsync();
                    return OperationResult<string>.BuildSuccessResult(update.Result);
                }
            }
            return OperationResult<string>.BuildFailure(deleteCategoryProperty.ErrorMessage);
        }
    }

    public class GetAllCategoryPropertyCommandHandlers : IRequestHandler<GetAllCategoryPropertyCommand, OperationResult<IEnumerable<CategoryProperty>>>
    {
        private readonly IDomainUnitOfWork unitOfWork;

        public GetAllCategoryPropertyCommandHandlers(IDomainUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }
        public async Task<OperationResult<IEnumerable<CategoryProperty>>> Handle(GetAllCategoryPropertyCommand request, CancellationToken cancellationToken)
        {
            var getAllCategoryProperty = await unitOfWork.CategoryRepository.CategoryPropertyRepository.GetAllCategoryPropertybyCategoryId(request.CategoryPropertyId, cancellationToken);
            if (getAllCategoryProperty.Success)
            {
                return OperationResult<IEnumerable<CategoryProperty>>.BuildSuccessResult(getAllCategoryProperty.Result);
            }
            return OperationResult<IEnumerable<CategoryProperty>>.BuildFailure(getAllCategoryProperty.ErrorMessage);
        }
    }
}
