using Common.LifeTime;
using Common.Operation;
using DAL.EF.Context;
using Domain.Aggregate.DomainAggregates.PostMagAggregate;
using Microsoft.EntityFrameworkCore;
using SiteService.Repositories.PostManagazineRepositorys.Contract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SiteService.Repositories.PostManagazineRepositorys.Implement
{
    public class PostMagazineRepository : IScoped, IPostMagazineRepository
    {
        private readonly FilmstanContext context;

        private DbSet<PostMagazine> PostMagazinesEntitie { get; set; }
        public PostMagazineRepository(FilmstanContext context)
        {
            this.context = context;
            PostMagazinesEntitie = context.Set<PostMagazine>();
        }

        public async Task<OperationResult<string>> AddPostManagazinAsync(PostMagazine postMagazine, CancellationToken cancellationToken)
        {
            try
            {
                await context.AddAsync(postMagazine, cancellationToken);
                return OperationResult<string>.BuildSuccessResult("Success Add Post");
            }
            catch (Exception ex)
            {
                return OperationResult<string>.BuildFailure(ex.Message);
            }
        }

        public OperationResult<string> Update(PostMagazine postMagazine, CancellationToken cancellationToken)
        {
            try
            {
                context.Update(postMagazine);
                return OperationResult<string>.BuildSuccessResult("Success Update Post");
            }
            catch (Exception ex)
            {
                return OperationResult<string>.BuildFailure(ex.Message);
            }
        }

        public async Task<OperationResult<PostMagazine>> GetPostById(Guid id, CancellationToken cancellationToken)
        {
            try
            {
                var findPost = await PostMagazinesEntitie.Where(x => x.Id == id).FirstOrDefaultAsync();
                return OperationResult<PostMagazine>.BuildSuccessResult(findPost);
            }
            catch (Exception ex)
            {
                return OperationResult<PostMagazine>.BuildFailure(ex.Message);
            }
        }

        public async Task<OperationResult<IEnumerable<PostMagazine>>> GetAllPostMagazine( CancellationToken cancellationToken)
        {
            try
            {
                var findPost = await PostMagazinesEntitie.ToListAsync();
                return OperationResult<IEnumerable<PostMagazine>>.BuildSuccessResult(findPost);
            }
            catch (Exception ex)
            {
                return OperationResult<IEnumerable<PostMagazine>>.BuildFailure(ex.Message);
            }
        }

        public async Task<OperationResult<IEnumerable<PostMagazine>>> GetAllPostMagazineByCategory(Guid cateId, CancellationToken cancellationToken)
        {
            try
            {
                var findPost = await PostMagazinesEntitie.Where(x => x.CategoryId == cateId).ToListAsync();
                return OperationResult<IEnumerable<PostMagazine>>.BuildSuccessResult(findPost);
            }
            catch (Exception ex)
            {
                return OperationResult<IEnumerable<PostMagazine>>.BuildFailure(ex.Message);
            }
        }

        public async Task<OperationResult<IEnumerable<PostMagazine>>> GetAllPostMagazineByWriteId(Guid writeId, CancellationToken cancellationToken)
        {
            try
            {
                var findPost = await PostMagazinesEntitie.Where(x => x.WriterId == writeId).ToListAsync();
                return OperationResult<IEnumerable<PostMagazine>>.BuildSuccessResult(findPost);
            }
            catch (Exception ex)
            {
                return OperationResult<IEnumerable<PostMagazine>>.BuildFailure(ex.Message);
            }
        }
    }
}
