using Common.Operation;
using Domain.Aggregate.DomainAggregates.CategoryAggregate;
using MediatR;
using Org.BouncyCastle.Ocsp;
using Query.BrandQueries;
using SiteService.Repositories.Implementation;
using System;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace QueryHandler.BrandsQueryHandlers
{
    public class GetBrandByIdQueryHandler : IRequestHandler<GetBrandById, OperationResult<Brand>>
    {
        private readonly IDomainUnitOfWork unitOfWork;

        public GetBrandByIdQueryHandler(IDomainUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public async Task<OperationResult<Brand>> Handle(GetBrandById request, CancellationToken cancellationToken)
        {
            var getbrand = await unitOfWork.CategoryRepository.BrandRepository.GetBrandById(request.brandId, cancellationToken);
            if (getbrand.Success)
            {
                return OperationResult<Brand>.BuildSuccessResult(getbrand.Result);
            }
            return OperationResult<Brand>.BuildFailure(getbrand.ErrorMessage);
        }
    }
}
