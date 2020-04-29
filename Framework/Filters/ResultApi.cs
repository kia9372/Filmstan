using Framework.ResponseFormatter.ResultApi;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Framework.Filters
{
    public class ResultApi : IResultFilter
    {
        private readonly IEnumerable<ResposeFormat> resposeFormat;

        public ResultApi(IEnumerable<ResposeFormat> resposeFormat)
        {
            this.resposeFormat = resposeFormat;
        }

        public void OnResultExecuted(ResultExecutedContext context)
        {
            
        }

        public  void OnResultExecuting(ResultExecutingContext context)
        {
            // Find Type of ResultContext
            Type resultType = context.Result.GetType();
            // Find Abstract Class For Equal by this Result Type
            ResposeFormat appropriateFormatter = resposeFormat
                .SingleOrDefault(formatter => formatter.ResultTypeToFormat == resultType);

            appropriateFormatter.ContextResult(context);
        }
    }
}
