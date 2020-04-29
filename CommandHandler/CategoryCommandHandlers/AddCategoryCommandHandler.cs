using Command.CategoryCommands;
using Common.Operation;
using Domain.Aggregate.DomainAggregates.CategoryAggregate;
using MediatR;
using SiteService.Repositories.Implementation;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CommandHandler.CategoryCommandHandlers
{
    public class AddCategoryCommandHandler : IRequestHandler<CreateCategoryCommand, OperationResult<string>>
    {
        private readonly IDomainUnitOfWork unitOfWork;

        public AddCategoryCommandHandler(IDomainUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }
        public async Task<OperationResult<string>> Handle(CreateCategoryCommand request, CancellationToken cancellationToken)
        {
            try
            {
                Category category = new Category(request.Name, request.ParentId);
                var addCategory = await unitOfWork.CategoryRepository.AddCategoryAsync(category, cancellationToken);
                if (addCategory.Success)
                {
                    await unitOfWork.CommitSaveChangeAsync();
                    return OperationResult<string>.BuildSuccessResult(addCategory.Result);
                }
                return OperationResult<string>.BuildFailure(addCategory.ErrorMessage);
            }
            catch (Exception ex)
            {
                return OperationResult<string>.BuildFailure(ex.Message);
            }
        }
    }
}

