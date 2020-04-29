using Common.Operation;
using Domain.Aggregate.DomainAggregates.PostMagAggregate;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace SiteService.Repositories.PostManagazineRepositorys.Contract
{
    public interface IPostMagazineRepository
    {
        Task<OperationResult<string>> AddPostManagazinAsync(PostMagazine postMagazine, CancellationToken cancellationToken);
        Task<OperationResult<IEnumerable<PostMagazine>>> GetAllPostMagazine( CancellationToken cancellationToken);
        Task<OperationResult<IEnumerable<PostMagazine>>> GetAllPostMagazineByCategory(Guid cateId, CancellationToken cancellationToken);
        Task<OperationResult<IEnumerable<PostMagazine>>> GetAllPostMagazineByWriteId(Guid writeId, CancellationToken cancellationToken);
        Task<OperationResult<PostMagazine>> GetPostById(Guid id, CancellationToken cancellationToken);
        OperationResult<string> Update(PostMagazine postMagazine, CancellationToken cancellationToken);
    }
}