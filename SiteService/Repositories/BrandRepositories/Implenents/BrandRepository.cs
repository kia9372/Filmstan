using Common.LifeTime;
using Common.Operation;
using DAL.EF.Context;
using DataTransfer;
using DataTransfer.BrandDtos;
using DataTransfer.RoleDtos;
using Domain.Aggregate.DomainAggregates.CategoryAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Migrations.Operations;
using Sieve.Models;
using Sieve.Services;
using SiteService.Repositories.BrandRepositories.Contract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SiteService.Repositories.BrandRepositories.Implenents
{
    public class BrandRepository : IScoped, IBrandRepository
    {
        private readonly FilmstanContext context;
        private readonly ISieveProcessor sieveProcessor;
        private DbSet<Brand> Brands;

        public BrandRepository(FilmstanContext context, ISieveProcessor sieveProcessor)
        {
            this.context = context;
            this.sieveProcessor = sieveProcessor;
            Brands = context.Set<Brand>();
        }

        public async Task<OperationResult<string>> AddBrandAsync(Brand brand, CancellationToken cancellation)
        {
            try
            {
                await Brands.AddAsync(brand, cancellation);
                return OperationResult<string>.BuildSuccessResult("Add Brand");
            }
            catch (Exception ex)
            {
                return OperationResult<string>.BuildFailure(ex.Message);
            }
        }
        public OperationResult<string> UpdateBrandAsync(Brand brand, CancellationToken cancellation)
        {
            try
            {
                Brands.Update(brand);
                return OperationResult<string>.BuildSuccessResult("Update Brand");
            }
            catch (Exception ex)
            {
                return OperationResult<string>.BuildFailure(ex.Message);
            }
        }
        public async Task<OperationResult<IEnumerable<Brand>>> GetAllBrandsByCategoryId(Guid categoryId, CancellationToken cancellation)
        {
            try
            {
                var bradns = await Brands.Where(x => x.CategoryId == categoryId).ToListAsync();
                if (bradns != null)
                {
                    return OperationResult<IEnumerable<Brand>>.BuildSuccessResult(bradns);
                }
                return OperationResult<IEnumerable<Brand>>.BuildFailure("Not Found");
            }
            catch (Exception ex)
            {
                return OperationResult<IEnumerable<Brand>>.BuildFailure(ex.Message);
            }
        }
        public async Task<OperationResult<GetAllPaging<GetAllBrands>>> GetAllBrandPaging(GetAllFormQuery formQuery, CancellationToken cancellation)
        {
            try
            {
                var res = Brands.Select(c => new GetAllBrands
                {
                    BrandName = c.BrandName,
                    CategoryName = c.Category.Name,
                    Id = c.Id,
                    CategoryId=c.CategoryId,
                    IsoBrandName = c.IsoBrandName
                });
                var sieveModel = new SieveModel
                {
                    PageSize = formQuery.PageSize,
                    Filters = formQuery.Filters,
                    Page = formQuery.Page,
                    Sorts = formQuery.Sorts
                };
                var result = sieveProcessor.Apply(sieveModel, res);
                return OperationResult<GetAllPaging<GetAllBrands>>.BuildSuccessResult(new GetAllPaging<GetAllBrands>
                {
                    Records = result,
                    TotalCount = await Brands.CountAsync()
                });
            }
            catch (Exception ex)
            {
                return OperationResult<GetAllPaging<GetAllBrands>>.BuildFailure(ex.Message);
            }
        }
        public async Task<OperationResult<Brand>> GetBrandById(Guid brandId, CancellationToken cancellationToken)
        {
            try
            {
                var brand = await Brands.FirstOrDefaultAsync(x => x.Id == brandId);
                if (brand != null)
                {
                    return OperationResult<Brand>.BuildSuccessResult(brand);
                }
                return OperationResult<Brand>.BuildFailure("Bradn Notfound");
            }
            catch (Exception ex)
            {
                return OperationResult<Brand>.BuildFailure(ex.Message);
            }
        }

    }
}
