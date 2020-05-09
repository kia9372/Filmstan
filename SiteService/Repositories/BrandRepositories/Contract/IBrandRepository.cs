using Common.LifeTime;
using Common.Operation;
using DataTransfer;
using DataTransfer.BrandDtos;
using DataTransfer.RoleDtos;
using Domain.Aggregate.DomainAggregates.CategoryAggregate;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace SiteService.Repositories.BrandRepositories.Contract
{
    public interface IBrandRepository : IScoped
    {
        Task<OperationResult<string>> AddBrandAsync(Brand brand, CancellationToken cancellation);
        Task<OperationResult<GetAllPaging<GetAllBrands>>> GetAllBrandPaging(GetAllFormQuery formQuery, CancellationToken cancellation);        Task<OperationResult<IEnumerable<Brand>>> GetAllBrandsByCategoryId(Guid categoryId, CancellationToken cancellation);
        OperationResult<string> UpdateBrandAsync(Brand brand, CancellationToken cancellation);
        Task<OperationResult<Brand>> GetBrandById(Guid brandId, CancellationToken cancellationToken);
    }
}