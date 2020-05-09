using Common.SiteEnums;
using Framework.CActionResult;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text;

namespace Framework.ResponseFormatter.ResultApi
{
   public class UserLoginReultFormatter : ResposeFormat
    {
        public override Type ResultTypeToFormat => typeof(UserLoginResult);
        public override void ContextResult(ResultExecutingContext context)
        {
            if (context.Result is UserLoginResult userLoginResult)
                context.Result = new JsonResult(new ReturnResult(false, StatusCode.BadRequest, userLoginResult.Message));
        }
    }
}