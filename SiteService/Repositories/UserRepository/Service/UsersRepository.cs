using Common.LifeTime;
using Common.Operation;
using DAL.EF.Context;
using DataTransfer.TokenDtos;
using DataTransfer.UserInformationDtos;
using Domain.Aggregate.DomainAggregates.UserAggregate;
using Microsoft.EntityFrameworkCore;
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
        private DbSet<User> Users { get; }
        public IUserRoleRepository UsersRoleRepository { get; set; }
        public IActivationCodeRepository ActivationCodeRepository { get; set; }

        public UsersRepository(FilmstanContext context)
        {
            this.context = context;
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

        public async Task<OperationResult<User>> GetUserTokenInfo(Guid id)
        {
            try
            {
                var userInfo = await Users.Where(x => x.Id == id)
                                   .Include(x => x.UserRoles)
                                   .ThenInclude(x => x.Role)
                                   .ThenInclude(x => x.AccessLevels)
                                   .FirstOrDefaultAsync();
                return OperationResult<User>.BuildSuccessResult(userInfo);
            }
            catch (Exception ex)
            {
                return OperationResult<User>.BuildFailure(ex.Message);
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
                        Result = v.UserRoles.Where(x => x.UserId == id)
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
