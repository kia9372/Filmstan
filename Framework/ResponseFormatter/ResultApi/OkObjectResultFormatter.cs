using Common.SiteEnums;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Net;

namespace Framework.ResponseFormatter.ResultApi
{
    public class OkObjectResultFormatter : ResposeFormat
    {
        public override Type ResultTypeToFormat => typeof(OkObjectResult);
        public override void ContextResult(ResultExecutingContext context)
        {
            if (context.Result is OkObjectResult okObjectResult)
                context.Result = new JsonResult(new ReturnResult<object>(true, StatusCode.Success, okObjectResult.Value)) { StatusCode = (int?)HttpStatusCode.OK };
        }
    }
}
