using Common.FilmstanExtentions;
using Common.SiteEnums;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;

namespace Framework.ResponseFormatter.ResultApi
{
    public class ReturnResult
    {
        public bool IsSuccess { get; }
        public StatusCode StatusCode { get; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string Message { get; }

        public ReturnResult(bool IsSuccess, StatusCode StatusCode, string Message = null)
        {
            this.IsSuccess = IsSuccess;
            this.StatusCode = StatusCode;
            this.Message = Message ?? StatusCode.EnumToDisplayName();
        }
        #region Implicit Operators
        public static implicit operator ReturnResult(Microsoft.AspNetCore.Mvc.OkResult result)
        {
            return new ReturnResult(true, StatusCode.Success);
        }

        public static implicit operator ReturnResult(BadRequestResult result)
        {
            return new ReturnResult(false, StatusCode.BadRequest);
        }

        public static implicit operator ReturnResult(BadRequestObjectResult result)
        {
            var message = result.Value.ToString();
            if (result.Value is SerializableError errors)
            {
                var errorMessages = errors.SelectMany(p => (string[])p.Value).Distinct();
                message = string.Join(" | ", errorMessages);
            }
            return new ReturnResult(false, StatusCode.BadRequest, message);
        }

        public static implicit operator ReturnResult(ContentResult result)
        {
            return new ReturnResult(true, StatusCode.Success, result.Content);
        }

        public static implicit operator ReturnResult(NotFoundResult result)
        {
            return new ReturnResult(false, StatusCode.NotFound);
        }
        #endregion
    }

    public class ReturnResult<TData> : ReturnResult where TData : class
    {
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public TData Data { get; }

        public ReturnResult(bool IsSuccess, StatusCode StatusCode, TData Data, string Message = null) : base(IsSuccess, StatusCode, Message)
        {
            this.Data = Data;
        }

        #region Implicit Operators
        public static implicit operator ReturnResult<TData>(TData data)
        {
            return new ReturnResult<TData>(true, StatusCode.Success, data);
        }

        public static implicit operator ReturnResult<TData>(Microsoft.AspNetCore.Mvc.OkResult result)
        {
            return new ReturnResult<TData>(true, StatusCode.Success, null);
        }

        public static implicit operator ReturnResult<TData>(OkObjectResult result)
        {
            return new ReturnResult<TData>(true, StatusCode.Success, (TData)result.Value);
        }

        public static implicit operator ReturnResult<TData>(BadRequestResult result)
        {
            return new ReturnResult<TData>(false, StatusCode.BadRequest, null);
        }

        public static implicit operator ReturnResult<TData>(BadRequestObjectResult result)
        {
            var message = result.Value.ToString();
            if (result.Value is SerializableError errors)
            {
                var errorMessages = errors.SelectMany(p => (string[])p.Value).Distinct();
                message = string.Join(" | ", errorMessages);
            }
            return new ReturnResult<TData>(false, StatusCode.BadRequest, null, message);
        }

        public static implicit operator ReturnResult<TData>(ContentResult result)
        {
            return new ReturnResult<TData>(true, StatusCode.Success, null, result.Content);
        }

        public static implicit operator ReturnResult<TData>(NotFoundResult result)
        {
            return new ReturnResult<TData>(false, StatusCode.NotFound, null);
        }

        public static implicit operator ReturnResult<TData>(NotFoundObjectResult result)
        {
            return new ReturnResult<TData>(false, StatusCode.NotFound, (TData)result.Value);
        }
        #endregion
    }
}
