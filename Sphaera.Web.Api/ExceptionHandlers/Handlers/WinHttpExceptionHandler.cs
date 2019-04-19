using System;
using JetBrains.Annotations;

namespace Sphaera.Web.Api.ExceptionHandlers.Handlers
{
    [UsedImplicitly]
    public class WinHttpExceptionHandler : DefaultExceptionHandler
    {
        #region Constructor

        public WinHttpExceptionHandler()
        {
            Order = 15;
        }

        #endregion

        #region Protected members

        protected override bool CanHandle(Exception exception, object model)
        {
            return 0 == string.Compare(exception.GetType().ToString(), "System.Net.Http.WinHttpException", StringComparison.OrdinalIgnoreCase);
        }

        #endregion
    }
}