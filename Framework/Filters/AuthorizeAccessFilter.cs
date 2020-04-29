using Common.ErrorHandlingException;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;

namespace Framework.Filters
{
    public class AuthorizeAccessFilter : IAuthorizationFilter
    {
        private readonly string name;

        public AuthorizeAccessFilter(string name)
        {
            this.name = name;
        }
        public void OnAuthorization(AuthorizationFilterContext context)
        {
            if (!HasPermission(context))
            {
                context.Result = new UnauthorizedResult();
            }
            throw new FilmstanUnAccessException("You Can not Access This Action");
        }

        private bool HasPermission(AuthorizationFilterContext context)
        {
            var claims = (context.HttpContext.User.Identity as ClaimsIdentity).Claims;
            var permissions = claims.Where(x => x.Type == "Permission").Select(x => x.Value).ToList();
            var route = context.HttpContext.Request.RouteValues;
            var RouteRequest = $"{route["controller"]}:{route["action"]}";
            if (permissions.Where(x => x == RouteRequest).FirstOrDefault() != null)
            {
                return true;
            }
            return false;
        }
    }
}
