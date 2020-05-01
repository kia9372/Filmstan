using Common.LifeTime;
using Common.Operation;
using DAL.EF.Context;
using DataTransfer;
using DataTransfer.RoleDtos;
using DataTransfer.TokenDtos;
using DataTransfer.UserDtos;
using DataTransfer.UserInformationDtos;
using Domain.Aggregate.DomainAggregates.UserAggregate;
using Microsoft.EntityFrameworkCore;
using Sieve.Models;
using Sieve.Services;
using SiteService.Repositories.ActivationCodeRepositorys.Contract;
using SiteService.Repositories.ActivationCodeRepositorys.Implement;
using SiteService.Repositories.UserRepository.Contract;
using SiteService.Repositories.UserRoleRepositorys.Contract;
using SiteService.Repositories.UserRoleRepositorys.Implement;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SiteService.Repositories.UserRepository.Service
{
    public class UsersRepository : IUserRepository, IScoped
    {
        private readonly FilmstanContext context;
        private readonly ISieveProcessor sieveProcessor;

        private DbSet<User> Users { get; }
        public IUserRoleRepository UsersRoleRepository { get; set; }
        public IActivationCodeRepository ActivationCodeRepository { get; set; }

        public UsersRepository(FilmstanContext context, ISieveProcessor sieveProcessor)
        {
            this.context = context;
            this.sieveProcessor = sieveProcessor;
            Users = context.Set<User>();
            UsersRoleRepository = new UserRoleRepository(context);
            ActivationCodeRepository = new ActivationCodeRepository(context);
        }
        public async Task<OperationResult<string>> AddAsync(User role, CancellationToken cancellation)
        {
            try
            {
                await Users.AddAsync(role, cancellation);
                return OperationResult<string>.BuildSuccessResult("Success Add");
            }
            catch (Exception ex)
            {
                return OperationResult<string>.BuildFailure(ex.Message);
            }
        }

        public async Task<OperationResult<IEnumerable<User>>> GetAllUsersAsync(CancellationToken cancellation)
        {
            try
            {
                var users = await Users.ToListAsync();
                return OperationResult<IEnumerable<User>>.BuildSuccessResult(users);
            }
            catch (Exception ex)
            {
                return OperationResult<IEnumerable<User>>.BuildFailure(ex.Message);
            }
        }
        public async Task<OperationResult<GetAllPaging<UserPagingDto>>> GetAllCategoryPagingAsync(GetAllFormQuery formQuery, CancellationToken cancellation)
        {
            try
            {
                var user = Users.AsNoTracking().Select(c => new UserPagingDto
                {
                    Id=c.Id,
                    ConfirmPhoneNumber = c.ConfirmPhoneNumber,
                    DisplayName = $"{c.Name} {c.Family}",
                    IsActive = c.IsActive,
                    IsLockedEnd = c.IsLockedEnd,
                    Username = c.Username,
                    ConfirmEmail = c.ConfirmEmail,
                    UserInfos = new UserPagingInfo
                    {
                        AccountFaile = c.AccountFaile,
                        Email = c.Email,
                        Family = c.Family,
                        LockedEnd = c.LockedEnd,
                        Name = c.Name,
                        PhoneNumber = c.PhoneNumber,
                        RoleId = c.UserRoles.Role.Id,
                        RoleName = c.UserRoles.Role.Name
                    }
                });
                var sieveModel = new SieveModel
                {
                    PageSize = formQuery.PageSize,
                    Filters = formQuery.Filters,
                    Page = formQuery.Page,
                    Sorts = formQuery.Sorts
                };
                var result = sieveProcessor.Apply(sieveModel, user);
                return OperationResult<GetAllPaging<UserPagingDto>>.BuildSuccessResult(new GetAllPaging<UserPagingDto>
                {
                    Records = result,
                    TotalCount = await Users.CountAsync()
                });
            }
            catch (Exception ex)
            {
                return OperationResult<GetAllPaging<UserPagingDto>>.BuildFailure(ex);
            }
        }


        public async Task<OperationResult<User>> GetUserByEmailAsync(string email, CancellationToken cancellation)
        {
            try
            {
                var user = await Users.Where(x => x.Email == email).FirstOrDefaultAsync();
                return OperationResult<User>.BuildSuccessResult(user);
            }
            catch (Exception ex)
            {
                return OperationResult<User>.BuildFailure(ex.Message);
            }
        }

        public async Task<OperationResult<User>> GetUserByIdAsync(Guid key, CancellationToken cancellation)
        {
            try
            {
                var user = await Users.Where(x => x.Id == key).FirstOrDefaultAsync();
                return OperationResult<User>.BuildSuccessResult(user);
            }
            catch (Exception ex)
            {
                return OperationResult<User>.BuildFailure(ex.Message);
            }
        }

        public async Task<OperationResult<User>> GetUserByPhoneNumberAsync(string phoneNumber, CancellationToken cancellation)
        {
            try
            {
                var user = await Users.Where(x => x.PhoneNumber == phoneNumber).FirstOrDefaultAsync();
                return OperationResult<User>.BuildSuccessResult(user);
            }
            catch (Exception ex)
            {
                return OperationResult<User>.BuildFailure(ex.Message);
            }
        }

        public async Task<OperationResult<User>> GetUserByUsernameAsync(string userName, CancellationToken cancellation)
        {
            try
            {
                var user = await Users.Where(x => x.Username == userName).FirstOrDefaultAsync();
                return OperationResult<User>.BuildSuccessResult(user);
            }
            catch (Exception ex)
            {
                return OperationResult<User>.BuildFailure(ex.Message);
            }
        }

        public OperationResult<bool> Update(User role, CancellationToken cancellation)
        {
            try
            {
                Users.Update(role);
                return OperationResult<bool>.BuildSuccessResult(true);
            }
            catch (Exception ex)
            {
                return OperationResult<bool>.BuildFailure(ex.Message);
            }
        }

        public async Task<OperationResult<TokenInfo>> GetUserTokenInfo(Guid id)
        {
            try
            {
                var userInfo = await Users.Where(x => x.Id == id)
                    .Select(x => new
                    {
                        IsActive = x.IsActive,
                        UserName = x.Username,
                        UserSecurotyStamp = x.SecurityStamp,
                        UserId = x.Id,
                        Role = x.UserRoles.Role
                    }).Select(c => new TokenInfo
                    {
                        RoleId = c.Role.Id,
                        UserId = c.UserId,
                        UserName = c.UserName,
                        IsActive = c.IsActive,
                        UserSecurityStamp = c.UserSecurotyStamp,
                        RoleSecurityStamp = c.Role.SecurityStamp,
                        AccessLevels = c.Role.AccessLevels.Where(x => x.RoleId == c.Role.Id).Select(x => x.Access).ToList()
                    }).FirstOrDefaultAsync();
                return OperationResult<TokenInfo>.BuildSuccessResult(userInfo);
            }
            catch (Exception ex)
            {
                return OperationResult<TokenInfo>.BuildFailure(ex.Message);
            }
        }

        public async Task<OperationResult<UserInformationDto>> GetUserInformation(Guid id)
        {
            UserInformationDto yg = new UserInformationDto();
            try
            {
                var userInfo = await Users.Where(x => x.Id == id)
                    .Select(v => new
                    {
                        Result = v.UserRoles.Role.AccessLevels
                        .Select(i => new UserInformationDto
                        {
                            DispayName = $"{v.Name } {v.Family}",
                            AccessUnserInfos = i.Role.AccessLevels.Select(x => x.Access)
                        }).ToList()
                    }).FirstOrDefaultAsync();

                return OperationResult<UserInformationDto>.BuildSuccessResult(userInfo.Result.FirstOrDefault());
            }
            catch (Exception ex)
            {
                return OperationResult<UserInformationDto>.BuildFailure(ex.Message);
            }
        }
    }
}
