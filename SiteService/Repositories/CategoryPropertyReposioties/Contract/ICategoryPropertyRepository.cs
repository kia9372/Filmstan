using Common.Operation;
using Domain.Aggregate.DomainAggregates.CategoryAggregate;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace SiteService.Repositories.CategoryPropertyReposioties.Implements
{
    public interface ICategoryPropertyRepository
    {
        Task<OperationResult<string>> AddBulkCategoryProperty(List<CategoryProperty> category, CancellationToken cancellation);
        Task<OperationResult<IEnumerable<CategoryProperty>>> GetAllCategoryPropertybyCategoryId(Guid guid, CancellationToken cancellation);
        Task<OperationResult<string>> UpdateBulkCategoryProperty(List<CategoryProperty> category, CancellationToken cancellation);
        OperationResult<string> UpdateCategoryProperty(CategoryProperty category, CancellationToken cancellation);
        Task<OperationResult<CategoryProperty>> GetCategoryPropertybyCategoryId(Guid guid, CancellationToken cancellation);
        Task<OperationResult<string>> DeleteBulkCategoryProperty(List<CategoryProperty> category, CancellationToken cancellation);
    }
}