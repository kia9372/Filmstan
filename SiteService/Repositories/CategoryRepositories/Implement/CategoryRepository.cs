using Common.LifeTime;
using Common.Operation;
using DAL.EF.Context;
using DataTransfer;
using DataTransfer.RoleDtos;
using Domain.Aggregate.DomainAggregates.CategoryAggregate;
using Microsoft.EntityFrameworkCore;
using Sieve.Models;
using Sieve.Services;
using SiteService.Repositories.BrandRepositories.Contract;
using SiteService.Repositories.BrandRepositories.Implenents;
using SiteService.Repositories.CategoryPropertyReposioties.Implements;
using SiteService.Repositories.CategoryRepositories.Contract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SiteService.Repositories.CategoryRepositories.Implement
{
    public class CategoryRepository : IScoped, ICategoryRepository
    {
        private readonly FilmstanContext context;
        private readonly ISieveProcessor sieveProcessor;

        private DbSet<Category> CategoryEntite { get; set; }
        public ICategoryPropertyRepository CategoryPropertyRepository { get; set; }
        public IBrandRepository BrandRepository { get; set; }

        public CategoryRepository(FilmstanContext context, ISieveProcessor sieveProcessor)
        {
            this.context = context;
            this.sieveProcessor = sieveProcessor;
            CategoryEntite = context.Set<Category>();
            CategoryPropertyRepository = new CategoryPropertyRepository(context);
            BrandRepository = new BrandRepository(context, sieveProcessor);
        }

        public async Task<OperationResult<string>> AddCategoryAsync(Category category, CancellationToken cancellation)
        {
            try
            {
                await context.AddAsync(category, cancellation);
                return OperationResult<string>.BuildSuccessResult("Success Add Category");
            }
            catch (Exception ex)
            {
                return OperationResult<string>.BuildFailure(ex.Message);
            }
        }

        public OperationResult<string> UpdateCategory(Category category, CancellationToken cancellation)
        {
            try
            {
                context.Update(category);
                return OperationResult<string>.BuildSuccessResult("Success Update Category");
            }
            catch (Exception ex)
            {
                return OperationResult<string>.BuildFailure(ex.Message);
            }
        }

        public async Task<OperationResult<IEnumerable<Category>>> GetAllCategoryAsync(CancellationToken cancellation)
        {
            try
            {
                var categories = await CategoryEntite.AsNoTracking().ToListAsync();
                return OperationResult<IEnumerable<Category>>.BuildSuccessResult(categories);
            }
            catch (Exception ex)
            {
                return OperationResult<IEnumerable<Category>>.BuildFailure(ex.Message);
            }
        }
        public async Task<OperationResult<GetAllPaging<Category>>> GetAllCategoryPagingAsync(GetAllFormQuery formQuery, CancellationToken cancellation)
        {
            try
            {
                var role = CategoryEntite.AsNoTracking();
                var sieveModel = new SieveModel
                {
                    PageSize = formQuery.PageSize,
                    Filters = formQuery.Filters,
                    Page = formQuery.Page,
                    Sorts = formQuery.Sorts
                };
                var result = sieveProcessor.Apply(sieveModel, role);
                return OperationResult<GetAllPaging<Category>>.BuildSuccessResult(new GetAllPaging<Category>
                {
                    Records = result.AsEnumerable(),
                    TotalCount = await CategoryEntite.CountAsync()
                });
            }
            catch (Exception ex)
            {
                return OperationResult<GetAllPaging<Category>>.BuildFailure(ex);
            }
        }

        public async Task<OperationResult<Category>> GetCategoryByIdAsync(Guid id, CancellationToken cancellation)
        {
            try
            {
                var category = await CategoryEntite.AsNoTracking().FirstOrDefaultAsync(x => x.Id == id);
                return OperationResult<Category>.BuildSuccessResult(category);
            }
            catch (Exception ex)
            {
                return OperationResult<Category>.BuildFailure(ex.Message);
            }
        }
    }
}
