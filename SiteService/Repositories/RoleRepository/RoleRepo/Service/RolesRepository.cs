using Common.LifeTime;
using Common.Operation;
using Common.RedisThriedLevelCach;
using DAL.EF.Context;
using DataTransfer;
using DataTransfer.RoleDtos;
using Domain.Aggregate.DomainAggregates.RoleAggregate;
using Microsoft.EntityFrameworkCore;
using Sieve.Models;
using Sieve.Services;
using SiteService.Repositories.RoleRepository.AccessLevels.Contract;
using SiteService.Repositories.RoleRepository.AccessLevels.Service;
using SiteService.Repositories.RoleRepository.RoleRepo.Contract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SiteService.Repositories.RoleRepository.RoleRepo.Service
{
    public class RolesRepository : IRoleRepository, IScoped
    {
        public IAccessLevelRepository AccessLevelRepository { get; set; }
        private readonly FilmstanContext context;
        private readonly ISieveProcessor sieveProcessor;

        private DbSet<Role> RoleEntite { get; set; }
        /// <summary>
        /// RoleRepository
        /// </summary>
        /// <param name="context"></param>
        public RolesRepository(FilmstanContext context, ISieveProcessor sieveProcessor)
        {
            this.context = context;
            this.sieveProcessor = sieveProcessor;
            AccessLevelRepository = new AccessLevelsRpostiory(context);
            RoleEntite = context.Set<Role>();
        }
        /// <summary>
        /// AddAsync
        /// </summary>
        /// <param name="role"></param>
        /// <param name="cancellation"></param>
        /// <returns></returns>
        public async Task<OperationResult<string>> AddAsync(Role role, CancellationToken cancellation)
        {
            try
            {
                await RoleEntite.AddAsync(role, cancellation);
                return OperationResult<string>.BuildSuccessResult("Success Add");
            }
            catch (Exception ex)
            {
                return OperationResult<string>.BuildFailure(ex);
            }
        }
        /// <summary>
        /// GetRoleByIdAsync
        /// </summary>
        /// <param name="key"></param>
        /// <param name="cancellation"></param>
        /// <returns></returns>
        public async Task<OperationResult<Role>> GetRoleByIdAsync(Guid key, CancellationToken cancellation)
        {
            try
            {
                var role = await RoleEntite.AsNoTracking().FirstOrDefaultAsync(x => x.Id == key);
                return OperationResult<Role>.BuildSuccessResult(role);
            }
            catch (Exception ex)
            {
                return OperationResult<Role>.BuildFailure(ex.Message);
            }
        }
        /// <summary>
        /// GetAllRoleAsync
        /// </summary>
        /// <param name="cancellation"></param>
        /// <returns></returns>
        public async Task<OperationResult<GetAllPaging<Role>>> GetAllRolePagingAsync(GetAllFormQuery formQuery, CancellationToken cancellation)
        {
            try
            {
                var role = RoleEntite.AsNoTracking();
                var sieveModel = new SieveModel
                {
                    PageSize = formQuery.PageSize,
                    Filters = formQuery.Filters,
                    Page = formQuery.Page,
                    Sorts = formQuery.Sorts
                };
                var result = sieveProcessor.Apply(sieveModel, role);
                return OperationResult<GetAllPaging<Role>>.BuildSuccessResult(new GetAllPaging<Role>
                {
                    Records = result.AsEnumerable(),
                    TotalCount = await RoleEntite.CountAsync()
                });
            }
            catch (Exception ex)
            {
                return OperationResult<GetAllPaging<Role>>.BuildFailure(ex);
            }
        }
        /// <summary>
        /// Get All Role For Dropdown
        /// </summary>
        /// <param name="cancellation"></param>
        /// <returns></returns>
        public async Task<OperationResult<IEnumerable<Role>>> GetAllRole(CancellationToken cancellation)
        {
            try
            {
                var role = await RoleEntite.AsNoTracking().ToListAsync();
                return OperationResult<IEnumerable<Role>>.BuildSuccessResult(role);
            }
            catch (Exception ex)
            {
                return OperationResult<IEnumerable<Role>>.BuildFailure(ex);
            }
        }
        /// <summary>
        /// role
        /// </summary>
        /// <param name="role"></param>
        /// <param name="cancellation"></param>
        /// <returns></returns>
        public OperationResult<bool> Update(Role role, CancellationToken cancellation)
        {
            try
            {
                RoleEntite.Update(role);
                return OperationResult<bool>.BuildSuccessResult(true);
            }
            catch (Exception ex)
            {
                return OperationResult<bool>.BuildFailure(ex.Message);
            }
        }
    }
}
