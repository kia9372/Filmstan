using Common.SiteEnums;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text;

namespace Common.ErrorHandlingException
{
    public class FilmstanUnAccessException : FilmstanException
    {
        public FilmstanUnAccessException(string message) : base(message, StatusCode.UnAccess)
        {
        }
        public FilmstanUnAccessException(string message, HttpStatusCode httpStatusCode)
            : base(message, StatusCode.UnAccess, httpStatusCode)
        {
        }
        public FilmstanUnAccessException(string message, HttpStatusCode httpStatusCode, Exception exception)
            : base(message, StatusCode.UnAccess, httpStatusCode, exception)
        {
        }
    }
}
