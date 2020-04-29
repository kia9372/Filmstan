using Common.FilmstanExtentions;
using Common.Operation;
using MediatR;
using Query.AccessLevelQueries;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace QueryHandler.AccessLevelQueryHandlers
{
    public class GetPermissionListQueryHandler : IRequestHandler<GetPermissionListQuery, OperationResult<string>>
    {
        public Task<OperationResult<string>> Handle(GetPermissionListQuery request, CancellationToken cancellationToken)
        {
            return null;
        }
    }
}
