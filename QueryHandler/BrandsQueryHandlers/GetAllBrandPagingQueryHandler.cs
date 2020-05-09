using Common.Operation;
using DataTransfer;
using DataTransfer.BrandDtos;
using Domain.Aggregate.DomainAggregates.CategoryAggregate;
using MediatR;
using Query.BrandQueries;
using SiteService.Repositories.Implementation;
using System.Threading;
using System.Threading.Tasks;

namespace QueryHandler.BrandsQueryHandlers
{
    public class GetAllBrandPagingQueryHandler : IRequestHandler<GetAllBrandPaging, OperationResult<GetAllPaging<GetAllBrands>>>
    {
        private readonly IDomainUnitOfWork unitOfWork;

        public GetAllBrandPagingQueryHandler(IDomainUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public async Task<OperationResult<GetAllPaging<GetAllBrands>>> Handle(GetAllBrandPaging request, CancellationToken cancellationToken)
        {
            var getbrand = await unitOfWork.CategoryRepository.BrandRepository.GetAllBrandPaging(request, cancellationToken);
            if (getbrand.Success)
            {
                return OperationResult<GetAllPaging<GetAllBrands>>.BuildSuccessResult(getbrand.Result);
            }
            return OperationResult<GetAllPaging<GetAllBrands>>.BuildFailure(getbrand.ErrorMessage);
        }
    }
}
