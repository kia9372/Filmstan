using Common.LifeTime;
using Common.Operation;
using DAL.EF.Context;
using Domain.Aggregate.DomainAggregates.UserAggregate;
using Domain.Aggregate.DomainAggregates.UserAggregate.ValueObjects;
using Microsoft.EntityFrameworkCore;
using SiteService.Repositories.ActivationCodeRepositorys.Contract;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SiteService.Repositories.ActivationCodeRepositorys.Implement
{
    public class ActivationCodeRepository : IActivationCodeRepository, IScoped
    {
        private readonly FilmstanContext context;
        private DbSet<ActivationCode> ActivationCodes { get; set; }
        public ActivationCodeRepository(FilmstanContext context)
        {
            this.context = context;
            ActivationCodes = context.Set<ActivationCode>();
        }

        public async Task<OperationResult<Tuple<string, int>>> AddAsync(ActivationCode activation, CancellationToken cancellation)
        {
            try
            {
                var add = await ActivationCodes.AddAsync(activation, cancellation);
                return OperationResult<Tuple<string, int>>.BuildSuccessResult(new Tuple<string, int>(activation.HashCode, activation.ActivateCode.Code));
            }
            catch (Exception ex)
            {
                return OperationResult<Tuple<string, int>>.BuildFailure(ex.Message);
            }
        }

        public OperationResult<int> Remove(ActivationCode activation)
        {
            try
            {
                ActivationCodes.Remove(activation);
                return OperationResult<int>.BuildSuccessResult(1);
            }
            catch (Exception ex)
            {
                return OperationResult<int>.BuildFailure(ex.Message);
            }
        }

        public async Task<OperationResult<ActivationCode>> FindByCodeAsync(int code, CancellationToken cancellation)
        {
            try
            {
                var add = await ActivationCodes.FirstOrDefaultAsync(x => x.ActivateCode.Code == code);
                return OperationResult<ActivationCode>.BuildSuccessResult(add);
            }
            catch (Exception ex)
            {
                return OperationResult<ActivationCode>.BuildFailure(ex.Message);
            }
        }

        public async Task<OperationResult<ActivationCode>> FindByUserIdAsync(Guid userId, CancellationToken cancellation)
        {
            try
            {
                var add = await ActivationCodes.FirstOrDefaultAsync(x => x.UserId == userId);
                return OperationResult<ActivationCode>.BuildSuccessResult(add);
            }
            catch (Exception ex)
            {
                return OperationResult<ActivationCode>.BuildFailure(ex.Message);
            }
        }

        public async Task<OperationResult<ActivationCode>> FindByCodeTypeAsync(CodeTypes codeTypes, CancellationToken cancellation)
        {
            try
            {
                var add = await ActivationCodes.FirstOrDefaultAsync(x => x.CodeType.CodeTypes == codeTypes);
                return OperationResult<ActivationCode>.BuildSuccessResult(add);
            }
            catch (Exception ex)
            {
                return OperationResult<ActivationCode>.BuildFailure(ex.Message);
            }
        }

        public async Task<OperationResult<ActivationCode>> FindByCodeTypeAndCodeAsync(CodeTypes codeTypes, int code, CancellationToken cancellation)
        {
            try
            {
                var add = await ActivationCodes.FirstOrDefaultAsync(x => x.CodeType.CodeTypes == codeTypes && x.ActivateCode.Code == code);
                return OperationResult<ActivationCode>.BuildSuccessResult(add);
            }
            catch (Exception ex)
            {
                return OperationResult<ActivationCode>.BuildFailure(ex.Message);
            }
        }

        public async Task<OperationResult<ActivationCode>> FindByHashCodeAndCodeAsync(string hashCode, int code, CancellationToken cancellation)
        {
            try
            {
                var add = await ActivationCodes.FirstOrDefaultAsync(x => x.ActivateCode.Code == code && x.HashCode == hashCode);
                return OperationResult<ActivationCode>.BuildSuccessResult(add);
            }
            catch (Exception ex)
            {
                return OperationResult<ActivationCode>.BuildFailure(ex.Message);
            }
        }
    }
}
