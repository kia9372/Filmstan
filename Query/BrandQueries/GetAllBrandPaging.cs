using Common.Operation;
using DataTransfer;
using DataTransfer.BrandDtos;
using DataTransfer.RoleDtos;
using Domain.Aggregate.DomainAggregates.CategoryAggregate;
using MediatR;

namespace Query.BrandQueries
{
    public class GetAllBrandPaging : GetAllFormQuery, IRequest<OperationResult<GetAllPaging<GetAllBrands>>>
    {

    }
}
