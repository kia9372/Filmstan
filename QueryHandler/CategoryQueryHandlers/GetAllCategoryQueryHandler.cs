using Common.Operation;
using Domain.Aggregate.DomainAggregates.CategoryAggregate;
using MediatR;
using Query.CategoryQueries;
using SiteService.Repositories.Implementation;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace QueryHandler.CategoryQueryHandlers
{
    public class GetAllCategoryQueryHandler : IRequestHandler<GetAllCategoryQuery, OperationResult<IEnumerable<Category>>>
    {
        private readonly IDomainUnitOfWork unitOfWork;

        public GetAllCategoryQueryHandler(IDomainUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }
        public async Task<OperationResult<IEnumerable<Category>>> Handle(GetAllCategoryQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var getAllCatgory = await unitOfWork.CategoryRepository.GetAllCategoryAsync(cancellationToken);
                return OperationResult<IEnumerable<Category>>.BuildSuccessResult(getAllCatgory.Result);
            }
            catch (Exception ex)
            {
                return OperationResult<IEnumerable<Category>>.BuildFailure(ex.Message);
            }
        }
    }

}
