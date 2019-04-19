using System;
using Autofac;
using Sphaera.Web.Api.ExceptionHandlers.Exceptions;
using Sphaera.Web.Api.ExceptionHandlers.Interfaces;
using Sphaera.Web.Core;

namespace Sphaera.Web.Api.Extensions
{
	public static class ExceptionExtensions
	{
	    private static readonly Lazy<IExceptionHandlingService> ExceptionHandlingService = new Lazy<IExceptionHandlingService>(() => Startup.Container.Resolve<IExceptionHandlingService>());

        public static SimpleError Parse(this Exception ex)
		{
			var result = new SimpleError { StatusCode = 500 };

			try
			{
			    switch (ex)
			    {
			        case HttpResponseException exception:
			            result.StatusCode = exception.ResultCode;
			            break;

			        case InvalidOperationException _:
			            if (ex.TargetSite.Name.Equals("FindView", StringComparison.OrdinalIgnoreCase))
			                result.StatusCode = 404;
			            break;
			    }

                result.Message = ExceptionHandlingService.Value.TryHandle(ex);
				if (string.IsNullOrEmpty(result.Message))
				{
					result.Message = ex.Message;
				}

			    result.Exception = ex.GetType().ToString();
                result.Trace = "\r\n" + ex.StackTrace;
			}
			// ReSharper disable EmptyGeneralCatchClause
			catch {
			// ReSharper restore EmptyGeneralCatchClause
			}

			return result;
		}
	}
}