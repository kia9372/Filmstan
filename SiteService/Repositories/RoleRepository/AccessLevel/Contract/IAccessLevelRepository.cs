using Common.Operation;
using DataTransfer.RoleDtos;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SiteService.Repositories.RoleRepository.AccessLevel.Contract
{
    public interface IAccessLevelRepository
    {
        Task<OperationResult<string>> SetAccess(AccessLevelDto accessLevels);
    }
}
