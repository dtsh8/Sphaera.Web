using System;
using JetBrains.Annotations;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace Sphaera.Web.Api.ExceptionHandlers.Handlers
{
    [UsedImplicitly]
    public sealed class AgregationExceptionHandler : ExceptionHandler
    {
        #region Constructor

		public AgregationExceptionHandler()
        {
			Order = -101;
        }

        #endregion

        #region Protected members

        protected override bool CanHandle(Exception exception, object model)
        {
            return exception is AggregateException;
        }

        protected override ModelStateDictionary Handle(Exception exception, object model)
        {
			var result = new ModelStateDictionary();

			foreach (var e in ((AggregateException)exception).InnerExceptions)
	        {
		        if (e is AggregateException)
		        {
			        return Handle(e, model);
		        }

	            var errors = Next(e, model);
	            if (errors != null)
	            {
	                foreach (var error in errors)
	                foreach (var valueError in error.Value.Errors)
	                    result.AddModelError(error.Key, valueError.ErrorMessage);
	            }
	        }

			return result;
        }

        #endregion
    }
}