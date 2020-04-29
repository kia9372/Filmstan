using Common.LifeTime;
using Common.Operation;
using DAL.EF.Context;
using Domain.Aggregate.DomainAggregates.CategoryAggregate;
using Microsoft.EntityFrameworkCore;
using SiteService.Repositories.CategoryRepositories.Contract;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SiteService.Repositories.CategoryRepositories.Implement
{
    public class CategoryRepository : IScoped, ICategoryRepository
    {
        private readonly FilmstanContext context;
        private DbSet<Category> CategoryEntite { get; set; }

        public CategoryRepository(FilmstanContext context)
        {
            this.context = context;
            CategoryEntite = context.Set<Category>();
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
