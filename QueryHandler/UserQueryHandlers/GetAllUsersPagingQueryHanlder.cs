using Common.Operation;
using DataTransfer;
using DataTransfer.UserDtos;
using Domain.Aggregate.DomainAggregates.UserAggregate;
using MediatR;
using Query.UserQueries;
using SiteService.Repositories.Implementation;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace QueryHandler.UserQueryHandlers
{
    public class GetAllUsersPagingQueryHanlder : IRequestHandler<GetAllUserPagingQuery, OperationResult<GetAllPaging<UserPagingDto>>>
    {
        private readonly IDomainUnitOfWork unitOfWork;

        public GetAllUsersPagingQueryHanlder(IDomainUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }
        public async Task<OperationResult<GetAllPaging<UserPagingDto>>> Handle(GetAllUserPagingQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var getAllUser = await unitOfWork.UsersRepository.GetAllCategoryPagingAsync(request, cancellationToken);
                return OperationResult<GetAllPaging<UserPagingDto>>.BuildSuccessResult(getAllUser.Result);
            }
            catch (Exception ex)
            {
                return OperationResult<GetAllPaging<UserPagingDto>>.BuildFailure(ex.Message);
            }
        }
    }
}
