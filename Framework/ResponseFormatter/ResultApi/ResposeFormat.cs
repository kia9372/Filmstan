using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Text;

namespace Framework.ResponseFormatter.ResultApi
{
    public abstract class ResposeFormat
    {
        public abstract Type ResultTypeToFormat { get; }
        public abstract void ContextResult(ResultExecutingContext context);
    }
}
