using Common.Operation;
using Domain.Aggregate.DomainAggregates.CategoryAggregate;
using MediatR;
using Query.BrandQueries;
using SiteService.Repositories.Implementation;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace QueryHandler.BrandsQueryHandlers
{
    public class GetBrandByCategoryIdQueryHandler : IRequestHandler<GetBrandByCategoryId, OperationResult<IEnumerable<Brand>>>
    {
        private readonly IDomainUnitOfWork unitOfWork;

        public GetBrandByCategoryIdQueryHandler(IDomainUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public async Task<OperationResult<IEnumerable<Brand>>> Handle(GetBrandByCategoryId request, CancellationToken cancellationToken)
        {
            var getbrand = await unitOfWork.CategoryRepository.BrandRepository.GetAllBrandsByCategoryId(request.categoryId, cancellationToken);
            if (getbrand.Success)
            {
                return OperationResult<IEnumerable<Brand>>.BuildSuccessResult(getbrand.Result);
            }
            return OperationResult<IEnumerable<Brand>>.BuildFailure(getbrand.ErrorMessage);
        }
    }
}
