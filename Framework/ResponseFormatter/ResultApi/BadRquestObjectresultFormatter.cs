using Common.SiteEnums;
using Common.Utilitis;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;

namespace Framework.ResponseFormatter.ResultApi
{
    public class BadRquestObjectresultFormatter : ResposeFormat
    {
        public override Type ResultTypeToFormat => typeof(BadRequestObjectResult);
        private List<ValidationModelError> validationModelErrors = new List<ValidationModelError>();
        public override void ContextResult(ResultExecutingContext context)
        {
            StringBuilder stringBulder = new StringBuilder();
            if (context.Result is BadRequestObjectResult badRequestObjectResult)
            {
                var errorsModelState = context.ModelState
                      .Where(x => x.Value.Errors.Count > 0)
                      .ToDictionary(kv => kv.Key, kv => kv.Value.Errors.Select(x => x.ErrorMessage)).ToArray();
                if (errorsModelState.Length > 0)
                {
                    foreach (var error in errorsModelState)
                    {
                        foreach (var subError in error.Value)
                        {
                            validationModelErrors.Add(new ValidationModelError
                            {
                                Field = error.Key,
                                Description = subError
                            });

                        }
                    }
                }
                else
                {
                    validationModelErrors.Add(new ValidationModelError
                    {
                        Field = badRequestObjectResult.StatusCode.ToString(),
                        Description = badRequestObjectResult.Value.ToString()
                    });
                }

                context.Result = new JsonResult(new ReturnResult(false, StatusCode.BadRequest, JToken.Parse(validationModelErrors.Serializer()).ToString(Newtonsoft.Json.Formatting.Indented))) { StatusCode = (int?)HttpStatusCode.BadRequest };
            }
        }
    }
}
