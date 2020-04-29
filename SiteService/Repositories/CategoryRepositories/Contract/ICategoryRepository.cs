using Common.LifeTime;
using Common.Operation;
using Domain.Aggregate.DomainAggregates.CategoryAggregate;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace SiteService.Repositories.CategoryRepositories.Contract
{
    public interface ICategoryRepository : IScoped
    {
        Task<OperationResult<string>> AddCategoryAsync(Category category, CancellationToken cancellation);
        Task<OperationResult<IEnumerable<Category>>> GetAllCategoryAsync(CancellationToken cancellation);
        Task<OperationResult<Category>> GetCategoryByIdAsync(Guid id, CancellationToken cancellation);
        OperationResult<string> UpdateCategory(Category category, CancellationToken cancellation);
    }
}
