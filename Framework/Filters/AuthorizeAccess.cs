using Microsoft.AspNetCore.Mvc;
using System;

namespace Framework.Filters
{
    [AttributeUsage(AttributeTargets.All)]
    public class AuthorizeAccess : TypeFilterAttribute
    {
        public AuthorizeAccess(string name) : base(typeof(AuthorizeAccessFilter))
        {
            Arguments = new object[] { name };
        }
    }
}