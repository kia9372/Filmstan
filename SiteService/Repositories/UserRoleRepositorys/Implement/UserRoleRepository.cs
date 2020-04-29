using Common.LifeTime;
using Common.Operation;
using DAL.EF.Context;
using Domain.Aggregate.DomainAggregates.UserAggregate;
using Microsoft.EntityFrameworkCore;
using SiteService.Repositories.UserRoleRepositorys.Contract;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SiteService.Repositories.UserRoleRepositorys.Implement
{
    public class UserRoleRepository : IUserRoleRepository, IScoped
    {
        private readonly FilmstanContext context;
        public DbSet<UserRole> UserRoles { get; set; }
        public UserRoleRepository(FilmstanContext context)
        {
            this.context = context;
            UserRoles = context.Set<UserRole>();
        }


        public async Task<OperationResult<string>> AddAsync(UserRole userRole, CancellationToken cancellation)
        {
            try
            {
                await context.AddAsync(userRole, cancellation);
                return OperationResult<string>.BuildSuccessResult("Add Success");
            }
            catch (Exception ex)
            {
                return OperationResult<string>.BuildSuccessResult(ex.Message);
            }
        }

        public OperationResult<string> UpdateUserRole(UserRole userRole)
        {
            try
            {
                context.Update(userRole);
                return OperationResult<string>.BuildSuccessResult("Update Success");
            }
            catch (Exception ex)
            {
                return OperationResult<string>.BuildFailure(ex.Message);
            }
        }

        public async Task<OperationResult<UserRole>> GetByUserId(Guid userId)
        {
            try
            {
                var userRole = await UserRoles.FirstOrDefaultAsync(x => x.UserId == userId);
                if (userRole != null)
                {
                    return OperationResult<UserRole>.BuildSuccessResult(userRole);
                }
                return OperationResult<UserRole>.BuildFailure("User Role Not Found");
            }
            catch (Exception ex)
            {
                return OperationResult<UserRole>.BuildFailure(ex.Message);
            }
        }
    }
}
