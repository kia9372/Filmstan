using Common.LifeTime;
using Common.Operation;
using Domain.Aggregate.DomainAggregates.UserAggregate;
using Domain.Aggregate.DomainAggregates.UserAggregate.ValueObjects;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SiteService.Repositories.ActivationCodeRepositorys.Contract
{
    public interface IActivationCodeRepository : IScoped
    {
        Task<OperationResult<Tuple<string, int>>> AddAsync(ActivationCode activation, CancellationToken cancellation);
        Task<OperationResult<ActivationCode>> FindByCodeAsync(int code, CancellationToken cancellation);
        Task<OperationResult<ActivationCode>> FindByCodeTypeAndCodeAsync(CodeTypes codeTypes, int code, CancellationToken cancellation);
        Task<OperationResult<ActivationCode>> FindByCodeTypeAsync(CodeTypes codeTypes, CancellationToken cancellation);
        Task<OperationResult<ActivationCode>> FindByUserIdAsync(Guid userId, CancellationToken cancellation);
        Task<OperationResult<ActivationCode>> FindByHashCodeAndCodeAsync(string hashCode, int code, CancellationToken cancellation);
        OperationResult<int> Remove(ActivationCode activation);
    }
}
