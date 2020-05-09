using Common.LifeTime;
using Common.Operation;
using DAL.EF.Context;
using Domain.Aggregate.DomainAggregates.CategoryAggregate;
using EFCore.BulkExtensions;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace SiteService.Repositories.CategoryPropertyReposioties.Implements
{
    public class CategoryPropertyRepository : IScoped, ICategoryPropertyRepository
    {
        private readonly FilmstanContext context;
        private DbSet<CategoryProperty> CategoryProperties { get; set; }

        public CategoryPropertyRepository(FilmstanContext context)
        {
            this.context = context;
            CategoryProperties = context.Set<CategoryProperty>();
        }

        public async Task<OperationResult<string>> AddBulkCategoryProperty(List<CategoryProperty> category, CancellationToken cancellation)
        {
            try
            {
                await context.BulkInsertAsync(category);
                return OperationResult<string>.BuildSuccessResult("Succes Add");
            }
            catch (Exception ex)
            {
                return OperationResult<string>.BuildFailure(ex.Message);
            }
        }

        public async Task<OperationResult<string>> UpdateBulkCategoryProperty(List<CategoryProperty> category, CancellationToken cancellation)
        {
            try
            {
                await context.BulkUpdateAsync(category);
                return OperationResult<string>.BuildSuccessResult("Update Add");
            }
            catch (Exception ex)
            {
                return OperationResult<string>.BuildFailure(ex.Message);
            }
        }

        public async Task<OperationResult<string>> DeleteBulkCategoryProperty(List<CategoryProperty> category, CancellationToken cancellation)
        {
            try
            {
                await context.BulkDeleteAsync(category);
                return OperationResult<string>.BuildSuccessResult("Delete Success Full");
            }
            catch (Exception ex)
            {
                return OperationResult<string>.BuildFailure(ex.Message);
            }
        }

        public async Task<OperationResult<IEnumerable<CategoryProperty>>> GetAllCategoryPropertybyCategoryId(Guid guid, CancellationToken cancellation)
        {
            try
            {
                var listCateProperty = await CategoryProperties.Where(x => x.CategoryId == guid).ToListAsync();
                if (listCateProperty != null)
                {
                    return OperationResult<IEnumerable<CategoryProperty>>.BuildSuccessResult(listCateProperty);
                }
                return OperationResult<IEnumerable<CategoryProperty>>.BuildFailure("Not Found");
            }
            catch (Exception ex)
            {
                return OperationResult<IEnumerable<CategoryProperty>>.BuildFailure(ex.Message);
            }
        }

        public async Task<OperationResult<CategoryProperty>> GetCategoryPropertybyCategoryId(Guid guid, CancellationToken cancellation)
        {
            try
            {
                var CateProperty = await CategoryProperties.Where(x => x.Id == guid).FirstOrDefaultAsync();
                if (CateProperty != null)
                {
                    return OperationResult<CategoryProperty>.BuildSuccessResult(CateProperty);
                }
                return OperationResult<CategoryProperty>.BuildFailure("Not Found");
            }
            catch (Exception ex)
            {
                return OperationResult<CategoryProperty>.BuildFailure(ex.Message);
            }
        }

        public OperationResult<string> UpdateCategoryProperty(CategoryProperty category, CancellationToken cancellation)
        {
            try
            {
                context.Update(category);
                return OperationResult<string>.BuildSuccessResult("Update Add");
            }
            catch (Exception ex)
            {
                return OperationResult<string>.BuildFailure(ex.Message);
            }
        }


    }
}
