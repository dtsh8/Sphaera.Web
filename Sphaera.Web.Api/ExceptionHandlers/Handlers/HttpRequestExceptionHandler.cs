using System;
using System.Net.Http;
using JetBrains.Annotations;

namespace Sphaera.Web.Api.ExceptionHandlers.Handlers
{
    [UsedImplicitly]
    public class HttpRequestExceptionHandler : DefaultExceptionHandler
    {
        #region Constructor

        public HttpRequestExceptionHandler()
        {
            Order = 11;
        }

        #endregion

        #region Protected members

        protected override bool CanHandle(Exception exception, object model)
        {
            return exception is HttpRequestException;
        }

        #endregion
    }
}