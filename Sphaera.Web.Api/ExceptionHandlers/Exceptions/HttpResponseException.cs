using System;
using System.Net;

namespace Sphaera.Web.Api.ExceptionHandlers.Exceptions
{
    public class HttpResponseException : Exception
    {
        public int ResultCode { get; }

        public HttpResponseException(HttpStatusCode resultCode)
        {
            ResultCode = (int)resultCode;
        }
    }
}