using Common.LifeTime;

using Domain.Core.UnitOfWork;
using SiteService.Repositories.CategoryRepositories.Contract;
using SiteService.Repositories.PostManagazineRepositorys.Contract;
using SiteService.Repositories.RoleRepository.RoleRepo.Contract;
using SiteService.Repositories.SettingsRepository.Contract;
using SiteService.Repositories.UserRepository.Contract;
using System;
using System.Collections.Generic;
using System.Text;

namespace SiteService.Repositories.Implementation
{
    public interface IDomainUnitOfWork : IUnitOfWork, IScoped
    {
        public ISettingRepository SettingRepository { get; }
        public IRoleRepository RoleRepository { get; }
        public IUserRepository UsersRepository { get; }
        public IPostMagazineRepository PostMagazineRepository { get; }
        public ICategoryRepository CategoryRepository { get; }
    }
}
