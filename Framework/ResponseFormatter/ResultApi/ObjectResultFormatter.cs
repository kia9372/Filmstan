using Common.SiteEnums;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;

namespace Framework.ResponseFormatter.ResultApi
{
    public class ObjectResultFormatter : ResposeFormat
    {
        public override Type ResultTypeToFormat => typeof(ObjectResult);
        public override void ContextResult(ResultExecutingContext context)
        {
            if (context.Result is ObjectResult objectResult && objectResult.StatusCode == null
                && !(objectResult.Value is ReturnResult))
            {
                context.Result = new JsonResult(new ReturnResult<object>(true, StatusCode.Success, objectResult.Value)) { StatusCode = objectResult.StatusCode };
            }
        }
    }
}
