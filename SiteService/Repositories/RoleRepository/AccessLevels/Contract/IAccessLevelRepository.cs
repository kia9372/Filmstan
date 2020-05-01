using Common.Operation;
using DataTransfer.RoleDtos;
using Domain.Aggregate.DomainAggregates.RoleAggregate;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SiteService.Repositories.RoleRepository.AccessLevels.Contract
{
    public interface IAccessLevelRepository
    {
        Task<OperationResult<string>> SetAccess(AccessLevelDto accessLevels);
        IEnumerable<AccessLevel> GetAccessLevels(Guid roleId);
    }
}
