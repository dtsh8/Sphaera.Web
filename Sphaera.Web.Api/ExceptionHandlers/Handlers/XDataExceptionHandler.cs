using System;
using JetBrains.Annotations;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using XData.Exceptions;

namespace Sphaera.Web.Api.ExceptionHandlers.Handlers
{
    [UsedImplicitly]
    public class XDataExceptionHandler : ExceptionHandler
    {
        #region Constructor

        public XDataExceptionHandler()
        {
            Order = 100;
        }

        #endregion

        #region Protected members

        protected override bool CanHandle(Exception exception, object model)
        {
            return exception is XDataException;
        }

        protected override ModelStateDictionary Handle(Exception exception, object model)
        {
            var result = new ModelStateDictionary();
            result.AddModelError(string.Empty, exception.Message);
            return result;
        }

        #endregion
    }
}