using System;
using System.IO;
using JetBrains.Annotations;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace Sphaera.Web.Api.ExceptionHandlers.Handlers
{
    [UsedImplicitly]
    public class DefaultExceptionHandler : ExceptionHandler
    {
        #region Constructor

        public DefaultExceptionHandler()
        {
            Order = 0;
        }

        #endregion

        #region Protected members

        protected override bool CanHandle(Exception exception, object model)
        {
            return exception is InvalidDataException
                || exception is InvalidOperationException
                || exception is ArgumentException
                || exception is UriFormatException;
        }

        protected override ModelStateDictionary Handle(Exception exception, object model)
        {
            if (exception.InnerException != null)
            {
                var res = Next(exception.InnerException, model);
                if (res != null)
                    return res;
            }

            var result = new ModelStateDictionary();
            result.AddModelError(string.Empty, exception.Message);
            return result;
        }

        #endregion
    }
}