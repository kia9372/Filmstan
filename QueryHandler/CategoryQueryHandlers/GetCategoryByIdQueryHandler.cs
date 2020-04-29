using Common.Operation;
using Domain.Aggregate.DomainAggregates.CategoryAggregate;
using MediatR;
using Query.CategoryQueries;
using SiteService.Repositories.Implementation;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace QueryHandler.CategoryQueryHandlers
{
    public class GetCategoryByIdQueryHandler : IRequestHandler<GetCategoryByIdQuery, OperationResult<Category>>
    {
        private readonly IDomainUnitOfWork unitOfWork;

        public GetCategoryByIdQueryHandler(IDomainUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }
        public async Task<OperationResult<Category>> Handle(GetCategoryByIdQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var getAllCatgory = await unitOfWork.CategoryRepository.GetCategoryByIdAsync(request.Id,cancellationToken);
                return OperationResult<Category>.BuildSuccessResult(getAllCatgory.Result);
            }
            catch (Exception ex)
            {
                return OperationResult<Category>.BuildFailure(ex.Message);
            }
        }
    }
}
