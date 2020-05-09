using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Framework.CActionResult
{
    public class UserLoginResult : IActionResult
    {
        public readonly string Message;

        public UserLoginResult(string message)
        {
            this.Message = message;
        }
        public async Task ExecuteResultAsync(ActionContext context)
        {
            var objectResult = new ObjectResult(Message) { StatusCode = StatusCodes.Status100Continue };
            await objectResult.ExecuteResultAsync(context);
        }
    }
}
