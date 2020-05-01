using Common.LifeTime;
using Common.Operation;
using DataTransfer;
using DataTransfer.RoleDtos;
using Domain.Aggregate.DomainAggregates.RoleAggregate;
using SiteService.Repositories.RoleRepository.AccessLevels.Contract;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SiteService.Repositories.RoleRepository.RoleRepo.Contract
{
    public interface IRoleRepository : IScoped
    {
        IAccessLevelRepository AccessLevelRepository { get; set; }
        Task<OperationResult<string>> AddAsync(Role role, CancellationToken cancellation);
        OperationResult<bool> Update(Role role, CancellationToken cancellation);
        Task<OperationResult<GetAllPaging<Role>>> GetAllRolePagingAsync(GetAllFormQuery formQuery, CancellationToken cancellation);
        Task<OperationResult<Role>> GetRoleByIdAsync(Guid key, CancellationToken cancellation);
        Task<OperationResult<IEnumerable<Role>>> GetAllRole(CancellationToken cancellation);
    }
}
