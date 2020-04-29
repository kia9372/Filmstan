using Common.SiteEnums;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text;

namespace Common.ErrorHandlingException
{
    public class FilmstanException : Exception
    {
        public HttpStatusCode HttpStatusCode { get; set; }
        public StatusCode StatusCode { get; set; }

        public FilmstanException(string message, StatusCode statusCode) : base(message)
        {
            StatusCode = statusCode;
        }

        public FilmstanException(string message, StatusCode statusCode, HttpStatusCode httpStatusCode) : base(message)
        {
            StatusCode = statusCode;
            HttpStatusCode = httpStatusCode;
        }

        public FilmstanException(string message, StatusCode statusCode, HttpStatusCode httpStatusCode, Exception exception)
            : base(message, exception)
        {
            StatusCode = statusCode;
            HttpStatusCode = httpStatusCode;
        }
    }
}
