using Common.LifeTime;
using Common.Operation;
using DAL.EF.Repositories.RoleRepository;
using Domain.Aggregate.DomainAggregates.RoleAggregate;
using SiteService.Repositories.RoleRepository.AccessLevel.Contract;
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
        Task<OperationResult<IEnumerable<Role>>> GetAllRoleAsync(CancellationToken cancellation);
        Task<OperationResult<Role>> GetRoleByIdAsync(Guid key, CancellationToken cancellation);
    }
}
