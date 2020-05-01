using Common.LifeTime;
using DataTransfer.TokenDtos;
using Domain.Aggregate.DomainAggregates.UserAggregate;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SiteService.Services.Contract
{
    public interface ITokenService:IScoped
    {
        Task<string> GenerateToken(TokenInfo user);
    }
}
