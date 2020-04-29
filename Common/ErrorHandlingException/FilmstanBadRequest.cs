using Common.SiteEnums;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text;

namespace Common.ErrorHandlingException
{
    public class FilmstanBadRequest : FilmstanException
    {
        public FilmstanBadRequest(string message) : base(message, StatusCode.BadRequest)
        {
        }

        public FilmstanBadRequest(string message, HttpStatusCode httpStatusCode)
            : base(message, StatusCode.BadRequest, httpStatusCode)
        {
        }

        public FilmstanBadRequest(string message, HttpStatusCode httpStatusCode, Exception exception)
          : base(message, StatusCode.BadRequest, httpStatusCode, exception)
        {
        }

    }
}
