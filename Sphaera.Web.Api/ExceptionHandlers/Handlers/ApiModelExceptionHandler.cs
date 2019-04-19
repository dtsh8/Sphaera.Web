using System;
using System.Collections;
using JetBrains.Annotations;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Sphaera.Web.Api.ExceptionHandlers.Exceptions;

namespace Sphaera.Web.Api.ExceptionHandlers.Handlers
{
    [UsedImplicitly]
    public class ApiModelExceptionHandler : DefaultExceptionHandler
    {
        #region Constructor

        public ApiModelExceptionHandler()
        {
            Order = 12;
        }

        #endregion

        #region Protected members

        protected override bool CanHandle(Exception exception, object model)
        {
            return exception is ApiModelException;
        }

        protected override ModelStateDictionary Handle(Exception exception, object model)
        {
            var error = string.Empty;
            foreach (DictionaryEntry data in exception.Data)
                error += $"{data.Key}: {data.Value}\n";

            var result = base.Handle(exception, model);
            result.AddModelError(string.Empty, error);
            return result;
        }

        #endregion
    }
}