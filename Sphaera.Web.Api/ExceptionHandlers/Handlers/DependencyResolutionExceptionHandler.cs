using System;
using Autofac.Core;
using JetBrains.Annotations;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace Sphaera.Web.Api.ExceptionHandlers.Handlers
{
    [UsedImplicitly]
    public class DependencyResolutionExceptionHandler : ExceptionHandler
    {
        #region Constructor

        public DependencyResolutionExceptionHandler()
        {
            Order = -10;
        }

        #endregion

        #region Protected members

        protected override bool CanHandle(Exception exception, object model)
        {
            return exception is DependencyResolutionException;
        }

        protected override ModelStateDictionary Handle(Exception exception, object model)
        {
            if (exception.InnerException != null)
            {
                var innerException = exception.InnerException;

                exception = innerException as DependencyResolutionException;
                while (exception != null)
                {
                    if (exception.InnerException == null)
                        goto exit;

                    innerException = exception.InnerException;
                    exception = innerException as DependencyResolutionException;
                }
                return Next(innerException, model);
            }
exit:
            var result = new ModelStateDictionary();
            result.AddModelError(string.Empty, exception.Message);
            return result;
        }

        #endregion
    }
}