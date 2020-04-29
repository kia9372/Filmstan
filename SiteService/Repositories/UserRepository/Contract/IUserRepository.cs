using Common.LifeTime;
using Common.Operation;
using DataTransfer.TokenDtos;
using DataTransfer.UserInformationDtos;
using Domain.Aggregate.DomainAggregates.UserAggregate;
using Domain.Core.UnitOfWork;
using SiteService.Repositories.ActivationCodeRepositorys.Contract;
using SiteService.Repositories.UserRoleRepositorys.Contract;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SiteService.Repositories.UserRepository.Contract
{
    public interface IUserRepository : IScoped
    {
        #region Repositories
        public IUserRoleRepository UsersRoleRepository { get; set; }
        public IActivationCodeRepository ActivationCodeRepository { get; set; }
        #endregion
        Task<OperationResult<string>> AddAsync(User role, CancellationToken cancellation);
        OperationResult<bool> Update(User role, CancellationToken cancellation);
        Task<OperationResult<IEnumerable<User>>> GetAllUsersAsync(CancellationToken cancellation);
        Task<OperationResult<User>> GetUserByIdAsync(Guid key, CancellationToken cancellation);
        Task<OperationResult<User>> GetUserByEmailAsync(string email, CancellationToken cancellation);
        Task<OperationResult<User>> GetUserByUsernameAsync(string userName, CancellationToken cancellation);
        Task<OperationResult<User>> GetUserByPhoneNumberAsync(string phoneNumber, CancellationToken cancellation);
        Task<OperationResult<User>> GetUserTokenInfo(Guid id);
        Task<OperationResult<UserInformationDto>> GetUserInformation(Guid id);
    }
}
