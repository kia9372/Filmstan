using Common.Operation;
using DataTransfer;
using Domain.Aggregate.DomainAggregates.CategoryAggregate;
using MediatR;
using Query.CategoryQueries;
using SiteService.Repositories.Implementation;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace QueryHandler.CategoryQueryHandlers
{
    public class GetAllCategoryPagingQueryHandler : IRequestHandler<GetAllCategoryPagingQuery, OperationResult<GetAllPaging<Category>>>
    {
        private readonly IDomainUnitOfWork unitOfWork;

        public GetAllCategoryPagingQueryHandler(IDomainUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }
        public async Task<OperationResult<GetAllPaging<Category>>> Handle(GetAllCategoryPagingQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var getAllCatgory = await unitOfWork.CategoryRepository.GetAllCategoryPagingAsync(request, cancellationToken);
                return OperationResult<GetAllPaging<Category>>.BuildSuccessResult(getAllCatgory.Result);
            }
            catch (Exception ex)
            {
                return OperationResult<GetAllPaging<Category>>.BuildFailure(ex.Message);
            }
        }
    }

}
