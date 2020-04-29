using Framework.ResponseFormatter.ResultApi;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Text;

namespace Framework.Filters
{
    public class ResultApiFormatter : TypeFilterAttribute
    {
        public ResultApiFormatter() : base(typeof(ResultApi))
        {
        }
    }
}