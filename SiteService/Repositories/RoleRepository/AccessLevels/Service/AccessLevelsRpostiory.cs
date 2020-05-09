using Common.Operation;
using DAL.EF.Context;
using DataTransfer.RoleDtos;
using Domain.Aggregate.DomainAggregates.RoleAggregate;
using EFCore.BulkExtensions;
using Microsoft.EntityFrameworkCore;
using SiteService.Repositories.RoleRepository.AccessLevels.Contract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SiteService.Repositories.RoleRepository.AccessLevels.Service
{
    public class AccessLevelsRpostiory : IAccessLevelRepository
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
                    List<AccessLevel> accessLevelsL = new List<AccessLevel>();
                    foreach (var item in newAccess)
                    {

                        accessLevelsL.Add(new AccessLevel
                        {
                            Id = Guid.NewGuid(),
                            Access = item,
                            RoleId = accessLevels.RoleId
                        });
                    }
                    await context.BulkInsertAsync(accessLevelsL);
                }

                var removeItems = currentAccess.Except(accessLevels.Access).ToList();
                if (removeItems != null)
                {
                    List<AccessLevel> accessLevelsL = new List<AccessLevel>();
                    foreach (var item in removeItems)
                    {
                        var accClaim = currentRoleAccessValue.SingleOrDefault(x => x.Access == item);
                        if (accClaim != null)
                        {
                            accessLevelsL.Add(accClaim);
                        }
                    }
                    await context.BulkDeleteAsync(accessLevelsL);
                }

                return OperationResult<string>.BuildSuccessResult("SuccessAdd");
            }
            catch (Exception ex)
            {
                return OperationResult<string>.BuildFailure(ex);
            }
        }

        public IEnumerable<AccessLevel> GetAccessLevels(Guid roleId)
        {
            return AccessLevels.AsNoTracking().Where(x => x.RoleId == roleId).ToList();
        }
    }
}