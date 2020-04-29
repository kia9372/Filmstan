using Common.SiteEnums;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Net;

namespace Framework.ResponseFormatter.ResultApi
{
    public class BadRequestResultFormatter : ResposeFormat 
    {
        public override Type ResultTypeToFormat => typeof(BadRequestResult);
        public override void ContextResult(ResultExecutingContext context)
        {
            context.Result = new JsonResult(new ReturnResult(false, StatusCode.BadRequest)) { StatusCode = (int?)HttpStatusCode.BadRequest };
        }
    }
}
