using Common.SiteEnums;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text;

namespace Common.ErrorHandlingException
{
    public class FilmstanUnAuthourizeException : FilmstanException
    {
        public FilmstanUnAuthourizeException(string message) : base(message, StatusCode.UnAuthorize)
        {
        }

        public FilmstanUnAuthourizeException(string message, HttpStatusCode httpStatusCode)
            : base(message, StatusCode.UnAuthorize, httpStatusCode)
        {
        }

        public FilmstanUnAuthourizeException(string message, HttpStatusCode httpStatusCode, Exception exception)
            : base(message, StatusCode.UnAuthorize, httpStatusCode, exception)
        {
        }
    }
}
