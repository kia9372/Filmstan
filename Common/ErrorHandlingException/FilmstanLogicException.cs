using Common.SiteEnums;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text;

namespace Common.ErrorHandlingException
{
    public class FilmstanLogicException : FilmstanException
    {
        public FilmstanLogicException(string message) : base(message, StatusCode.LogicError)
        {
        }
        public FilmstanLogicException(string message, HttpStatusCode httpStatusCode)
            : base(message, StatusCode.LogicError, httpStatusCode)
        {
        }
        public FilmstanLogicException(string message, HttpStatusCode httpStatusCode, Exception exception)
            : base(message, StatusCode.LogicError, httpStatusCode, exception)
        {
        }
    }
}
