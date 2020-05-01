using Common.Operation;
using DataTransfer;
using DataTransfer.RoleDtos;
using Domain.Aggregate.DomainAggregates.RoleAggregate;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Query.RoleQueries
{
    public class GetRoleListPagingQuery :GetAllFormQuery, IRequest<OperationResult<GetAllPaging<Role>>>
    {
    }
}
