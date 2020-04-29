using Common.LifeTime;
using Common.Operation;
using DataTransfer.Setting;
using Domain.Aggregate.DomainAggregates.SettingAggregate;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SiteService.Repositories.SettingsRepository.Contract
{
    public interface ISettingRepository : IScoped
    {
        Task<OperationResult<string>> Set<T>(string key, T value, CancellationToken cancellationToken) where T : ISettings<T>, new();

        Task<OperationResult<T>> Get<T>(string @enum, CancellationToken cancellation) where T : ISettings<T>, new();
    }
}
