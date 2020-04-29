using Common.Operation;
using Domain.Aggregate.DomainAggregates.PostMagAggregate;
using MediatR;
using Query.PostMagazaineQueries;
using SiteService.Repositories.Implementation;
using System.Threading;
using System.Threading.Tasks;

namespace QueryHandler.PostMagazineQueryHandlers
{
    public class GetPostByIdQueryHandler : IRequestHandler<GetPostByIdQuery, OperationResult<PostMagazine>>
    {
        private readonly IDomainUnitOfWork unitOfWork;

        public GetPostByIdQueryHandler(IDomainUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }
        public async Task<OperationResult<PostMagazine>> Handle(GetPostByIdQuery request, CancellationToken cancellationToken)
        {
            var findPost = await unitOfWork.PostMagazineRepository.GetPostById(request.Id, cancellationToken);
            if (findPost.Result != null)
            {
                return OperationResult<PostMagazine>.BuildSuccessResult(findPost.Result);
            }
            return OperationResult<PostMagazine>.BuildFailure(findPost.ErrorMessage);
        }
    }
}
