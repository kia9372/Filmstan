using Common.LifeTime;
using Common.SiteEnums;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Net;

namespace Framework.ResponseFormatter.ResultApi
{
    public class OkResultFormatter : ResposeFormat 
    {
        public override Type ResultTypeToFormat => typeof(OkResult);
        public override void ContextResult(ResultExecutingContext context)
        {
            context.Result = new JsonResult(new ReturnResult(true, StatusCode.Success)) { StatusCode = (int?)HttpStatusCode.OK };
        }
    }
}
