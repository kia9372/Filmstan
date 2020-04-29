using Common.Operation;
using DAL.EF.Context;
using DataTransfer.RoleDtos;
using Domain.Aggregate.DomainAggregates.RoleAggregate;
using Microsoft.EntityFrameworkCore;
using SiteService.Repositories.RoleRepository.AccessLevel.Contract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DAL.EF.Repositories.RoleRepository
{
    internal class AccessLevelsRpostiory : IAccessLevelRepository
    {
        private FilmstanContext context;
        private DbSet<AccessLevel> AccessLevels { get; set; }

        public AccessLevelsRpostiory(FilmstanContext context)
        {
            this.context = context;
            AccessLevels = context.Set<AccessLevel>();
        }

        public async Task<OperationResult<string>> SetAccess(AccessLevelDto accessLevels)
        {
            try
            {
                var currentRoleAccessValue = GetAccessLevels(accessLevels.RoleId);
                var currentAccess = currentRoleAccessValue.Select(x => x.Access).ToList();
                var newAccess = accessLevels.Access.Except(currentAccess).ToList();

                if (newAccess != null)
                {
                    foreach (var item in newAccess)
                    {
                        context.Add(new AccessLevel
                        {
                            Access = item,
                            RoleId = accessLevels.RoleId
                        });
                    }
                }

                var removeItems = currentAccess.Except(accessLevels.Access).ToList();
                if (removeItems != null)
                {
                    foreach (var item in removeItems)
                    {
                        var accClaim = currentRoleAccessValue.SingleOrDefault(x => x.Access == item);
                        if (accClaim != null)
                        {
                            context.Remove(accClaim);
                        }
                    }
                }

                return OperationResult<string>.BuildSuccessResult("SuccessAdd");
            }
            catch (Exception ex)
            {
                return OperationResult<string>.BuildFailure(ex);
            }
        }

        private IEnumerable<AccessLevel> GetAccessLevels(Guid roleId)
        {
            return AccessLevels.AsNoTracking().Where(x => x.RoleId == roleId).ToList();
        }
    }
}