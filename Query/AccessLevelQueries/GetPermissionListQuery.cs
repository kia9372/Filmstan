using Common.Operation;
using MediatR;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace Query.AccessLevelQueries
{
    public class GetPermissionListQuery : IRequest<OperationResult<string>>
    {
        public Assembly Assembly { get; set; }
    }
}
