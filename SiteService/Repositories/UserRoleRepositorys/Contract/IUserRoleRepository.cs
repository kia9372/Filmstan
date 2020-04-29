using Common.LifeTime;
using Common.Operation;
using Domain.Aggregate.DomainAggregates.UserAggregate;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SiteService.Repositories.UserRoleRepositorys.Contract
{
    public interface IUserRoleRepository : IScoped
    {
        Task<OperationResult<string>> AddAsync(UserRole userRole, CancellationToken cancellation);
        OperationResult<string> UpdateUserRole(UserRole userRole);
        Task<OperationResult<UserRole>> GetByUserId(Guid userId);
    }
}
