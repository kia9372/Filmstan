﻿using Common.Operation;
using Domain.Aggregate.DomainAggregates.PostMagAggregate;
using MediatR;
using Query.PostMagazaineQueries;
using SiteService.Repositories.Implementation;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace QueryHandler.PostMagazineQueryHandlers
{
    public class GetAllPostByCategoryrIdQueryHandler : IRequestHandler<GetPostByCategoryIdQuery, OperationResult<IEnumerable<PostMagazine>>>
    {
        private readonly IDomainUnitOfWork unitOfWork;

        public GetAllPostByCategoryrIdQueryHandler(IDomainUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }
        public async Task<OperationResult<IEnumerable<PostMagazine>>> Handle(GetPostByCategoryIdQuery request, CancellationToken cancellationToken)
        {
            var findPost = await unitOfWork.PostMagazineRepository.GetAllPostMagazineByCategory(request.categoryId, cancellationToken);
            if (findPost.Result != null)
            {
                return OperationResult<IEnumerable<PostMagazine>>.BuildSuccessResult(findPost.Result);
            }
            return OperationResult<IEnumerable<PostMagazine>>.BuildFailure(findPost.ErrorMessage);
        }
    }
}
