using Common.Operation;
using DataTransfer;
using DataTransfer.RoleDtos;
using DataTransfer.UserDtos;
using Domain.Aggregate.DomainAggregates.UserAggregate;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Query.UserQueries
{
    public class GetAllUserPagingQuery : GetAllFormQuery, IRequest<OperationResult<GetAllPaging<UserPagingDto>>>
    {
    }
}
