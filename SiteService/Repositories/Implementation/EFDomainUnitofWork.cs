using Common.LifeTime;
using DAL.EF.Context;
using Domain.Core.UnitOfWork;
using SiteService.Repositories.CategoryRepositories.Contract;
using SiteService.Repositories.CategoryRepositories.Implement;
using SiteService.Repositories.PostManagazineRepositorys.Contract;
using SiteService.Repositories.PostManagazineRepositorys.Implement;
using SiteService.Repositories.RoleRepository.RoleRepo.Contract;
using SiteService.Repositories.RoleRepository.RoleRepo.Service;
using SiteService.Repositories.SettingsRepository.Contract;
using SiteService.Repositories.SettingsRepository.Service;
using SiteService.Repositories.UserRepository.Contract;
using SiteService.Repositories.UserRepository.Service;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SiteService.Repositories.Implementation
{
    public class EFDomainUnitofWork : IDomainUnitOfWork, IScoped
    {
        private readonly FilmstanContext context;

        public ISettingRepository SettingRepository { get; private set; }
        public IRoleRepository RoleRepository { get; private set; }
        public IUserRepository UsersRepository { get; private set; }
        public ICategoryRepository CategoryRepository { get; private set; }
        public IPostMagazineRepository PostMagazineRepository { get; }
        public EFDomainUnitofWork()
        {

        }

        public EFDomainUnitofWork(FilmstanContext context)
        {
            this.context = context;
            SettingRepository = new SettingRepository(context);
            RoleRepository = new RolesRepository(context);
            UsersRepository = new UsersRepository(context);
            CategoryRepository = new CategoryRepository(context);
            PostMagazineRepository = new PostMagazineRepository(context);
        }

        public void CommitSaveChange()
        {
            try
            {

                context.SaveChanges();
                context.Dispose();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task CommitSaveChangeAsync()
        {
            try
            {
                await context.SaveChangesAsync();
                await context.DisposeAsync();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
