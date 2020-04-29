using Common.Operation;
using Domain.Aggregate.DomainAggregates.PostMagAggregate;
using MediatR;
using Query.PostMagazaineQueries;
using SiteService.Repositories.Implementation;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace QueryHandler.PostMagazineQueryHandlers
{
    public class GetAllPostQueryHandler : IRequestHandler<GetAllPosQuery, OperationResult<IEnumerable<PostMagazine>>>
    {
        private readonly IDomainUnitOfWork unitOfWork;

        public GetAllPostQueryHandler(IDomainUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }
        public async Task<OperationResult<IEnumerable<PostMagazine>>> Handle(GetAllPosQuery request, CancellationToken cancellationToken)
        {
            var findPost = await unitOfWork.PostMagazineRepository.GetAllPostMagazine(cancellationToken);
            if (findPost.Result != null)
            {
                return OperationResult<IEnumerable<PostMagazine>>.BuildSuccessResult(findPost.Result);
            }
            return OperationResult<IEnumerable<PostMagazine>>.BuildFailure(findPost.ErrorMessage);
        }
    }
}
