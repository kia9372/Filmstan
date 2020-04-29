using Common.FilmStanEnums;
using Common.LifeTime;
using Common.Operation;
using Common.Utilitis;
using DAL.EF.Context;
using DataTransfer.Setting;
using Domain.Aggregate.DomainAggregates.SettingAggregate;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using SiteService.Repositories.SettingsRepository.Contract;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SiteService.Repositories.SettingsRepository.Service
{
    public class SettingRepository : ISettingRepository, IScoped
    {
        private readonly FilmstanContext context;
        private DbSet<Setting> Settings { get; set; }

        public SettingRepository(FilmstanContext context)
        {
            this.context = context;
            Settings = context.Set<Setting>();
        }
        /// <summary>
        /// Add Setting Async
        /// </summary>
        /// <param name="setting"></param>
        /// <param name="cancellation"></param>
        /// <returns></returns>
        public async Task<OperationResult<string>> Set<T>(string key, T value, CancellationToken cancellationToken) where T : ISettings<T>, new()
        {
            try
            {
                var jsonValue = JsonConvert.SerializeObject(value);
                var settings = await Settings.FirstOrDefaultAsync(x => x.Key == key);

                if (settings != null)
                {
                    settings.SetSettingValue(jsonValue);
                }
                else
                {
                    Setting stting = new Setting(key, jsonValue);
                    await Settings.AddAsync(stting, cancellationToken);
                }
                return OperationResult<string>.BuildSuccessResult("Success Add Setting");
            }
            catch (Exception ex)
            {
                return OperationResult<string>.BuildFailure(ex.Message);
            }
        }
        /// <summary>
        /// Get All Setting Async
        /// </summary>
        /// <param name="cancellation"></param>
        /// <returns></returns>
        public async Task<OperationResult<T>> Get<T>(string @enum, CancellationToken cancellation) where T : ISettings<T>, new()
        {
            var result = await Settings.AsNoTracking().FirstOrDefaultAsync(x => x.Key == @enum);
            if (result != null)
            {
                var val = result.SettingValue.Deserialize<T>();
                return OperationResult<T>.BuildSuccessResult(val);
            }
            return OperationResult<T>.BuildSuccessResult(new T().WithDefaultValues());

        }
    }
}
