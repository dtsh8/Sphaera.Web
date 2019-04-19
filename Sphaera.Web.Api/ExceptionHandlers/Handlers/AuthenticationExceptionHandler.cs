using System;
using System.Security.Authentication;
using Autofac;
using JetBrains.Annotations;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Sphaera.Web.Api.Resources;

namespace Sphaera.Web.Api.ExceptionHandlers.Handlers
{
    [UsedImplicitly]
    public sealed class AuthenticationExceptionHandler : ExceptionHandler
    {
        #region Private Field

        private static readonly Lazy<IHttpContextAccessor> ContextAccessor = new Lazy<IHttpContextAccessor>(() => Startup.Container.Resolve<IHttpContextAccessor>());

        #endregion

        #region Constructor

		public AuthenticationExceptionHandler()
		{
		    Order = -1000002;
		}

        #endregion

        #region Protected members

        protected override bool CanHandle(Exception exception, object model)
        {
			return exception is AuthenticationException;
        }

        protected override ModelStateDictionary Handle(Exception exception, object model)
        {
            var httpContext = ContextAccessor.Value.HttpContext;
			httpContext.SignOutAsync("Cookies");

            if (!httpContext.Response.HasStarted)
				httpContext.Response.Redirect(@"/Account/Login", true);

            var result = new ModelStateDictionary();
            result.AddModelError(string.Empty, Errors.Forbidden);
            return result;
        }

        #endregion
    }
}