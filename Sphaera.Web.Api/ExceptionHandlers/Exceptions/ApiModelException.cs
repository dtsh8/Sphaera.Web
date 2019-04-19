using System;
using System.Net;

namespace Sphaera.Web.Api.ExceptionHandlers.Exceptions
{
    public class ApiModelException : Exception
    {
        public HttpStatusCode StatusCode
        {
            get;
        }

        public ApiModelException(string error) : base(error) { }

        public ApiModelException(string error, Exception exception) : base(error, exception) { }

        public ApiModelException(string error, HttpStatusCode statusCode) : base(error)
        {
            StatusCode = statusCode;
        }
    }
}