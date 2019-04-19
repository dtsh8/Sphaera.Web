using System;
using System.Reflection;
using JetBrains.Annotations;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace Sphaera.Web.Api.ExceptionHandlers.Handlers
{
    [UsedImplicitly]
    public sealed class TargetInvocationExceptionHandler : ExceptionHandler
    {
        #region Constructor

        public TargetInvocationExceptionHandler()
        {
            Order = -100;
        }

        #endregion

        #region Protected members

        protected override bool CanHandle(Exception exception, object model)
        {
            return exception is TargetInvocationException && exception.InnerException != null;
        }

        protected override ModelStateDictionary Handle(Exception exception, object model)
        {
            return Next(exception.InnerException, model);
        }

        #endregion
    }
}