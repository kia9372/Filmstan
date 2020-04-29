using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.Extensions.Caching.Distributed;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Common.RedisThriedLevelCach
{
    public static class Redisabled
    {
        //public static IList<TEntity> CacheListToRedis<TEntity, TResult>(this IQueryable<TEntity> query, Func<IQueryable<TEntity>, TResult> materializer)
        //{
        //    string keyName = "GetAll" + query.ElementType.Name;
        //    var mat = materializer.Method.Name;
        //    return query.ToList();
        //}
    }
}
