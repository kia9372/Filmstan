using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Framework.ResponseFormatter.ResultApi
{
    public class FileStreamResultFormatter : ResposeFormat
    {
        public override Type ResultTypeToFormat => typeof(FileStreamResult);
        public override void ContextResult(ResultExecutingContext context)
        {
            if (context.Result is FileStreamResult fileStramReult)
                context.Result = fileStramReult;
        }
    }
}

