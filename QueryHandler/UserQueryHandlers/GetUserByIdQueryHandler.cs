using Common.Operation;
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
    public class GetUserByIdQueryHandler : IRequestHandler<GerUserByIdQuery, OperationResult<User>>
    {
        private readonly IDomainUnitOfWork unitOfWork;

        public GetUserByIdQueryHandler(IDomainUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }
        public async Task<OperationResult<User>> Handle(GerUserByIdQuery request, CancellationToken cancellationToken)
        {
            var result = await unitOfWork.UsersRepository.GetUserByIdAsync(request.Id, cancellationToken);
            if (result.Success)
            {
                return OperationResult<User>.BuildSuccessResult(result.Result);
            }
            return OperationResult<User>.BuildFailure(result.ErrorMessage);
        }
    }
}
