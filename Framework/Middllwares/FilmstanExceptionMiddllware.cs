using Common.ErrorHandlingException;
using Common.SiteEnums;
using Common.Utilitis;
using Framework.ResponseFormatter.ResultApi;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Framework.Middllwares
{
    public class FilmstanExceptionMiddllware
    {
        private readonly RequestDelegate next;

        public FilmstanExceptionMiddllware(RequestDelegate next)
        {
            this.next = next;
        }

        public async Task Invoke(HttpContext httpContext)
        {
            HttpStatusCode httpStatusCode = HttpStatusCode.Unauthorized;
            StatusCode statusCode = StatusCode.UnAuthorize;
            string message = "";
            try
            {
                await next(httpContext);
            }
            catch (FilmstanUnAccessException ex)
            {
                message = ex.Message;
                statusCode = ex.StatusCode;
                SetUnAccessStatusCode();
                await ReponseWriteAsync();
            }
            catch (FilmstanUnAuthourizeException ex)
            {
                message = ex.Message;
                statusCode = ex.StatusCode;
                SetUnAthurizedStatusCode();
                await ReponseWriteAsync();
            }
            catch (FilmstanException ex)
            {
                message = ex.Message;
                statusCode = ex.StatusCode;
                await ReponseWriteAsync();
            }
            catch (Exception ex)
            {
                var result = new ReturnResult(false, Common.SiteEnums.StatusCode.ServerError, ex.Message);
                await httpContext.Response.WriteAsync(result.Serializer());
            }
            async Task ReponseWriteAsync()
            {
                httpContext.Response.StatusCode = (int)httpStatusCode;
                httpContext.Response.ContentType = "application/json";
                var result = new ReturnResult(false, statusCode, message);
                await httpContext.Response.WriteAsync(result.Serializer());
            }
            void SetUnAthurizedStatusCode()
            {
                httpStatusCode = HttpStatusCode.Unauthorized;
                statusCode = StatusCode.UnAuthorize;
            }
            void SetUnAccessStatusCode()
            {
                httpStatusCode = HttpStatusCode.Forbidden;
                statusCode = StatusCode.UnAccess;
            }
        }
    }
}
