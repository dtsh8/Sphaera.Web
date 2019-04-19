using System;
using JetBrains.Annotations;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Sphaera.Web.Api.ExceptionHandlers.Interfaces;

namespace Sphaera.Web.Api.ExceptionHandlers
{
    public abstract class ExceptionHandler : IExceptionHandler
    {
        #region private members

        private IExceptionHandler _next;

        #endregion

        #region IExceptionHandler members

        public int Order { get; protected set; }

        void IExceptionHandler.Initialize(IExceptionHandler next)
        {
            _next = next;
        }

        ModelStateDictionary IExceptionHandler.Handle(Exception exception, object model)
        {
            return CanHandle(exception, model) ? Handle(exception, model) : Next(exception, model);
        }

        #endregion

        #region Protected members

        protected ModelStateDictionary Next([NotNull] Exception exception, [CanBeNull] object model)
        {
            return _next?.Handle(exception, model);
        }

        protected abstract bool CanHandle([NotNull] Exception exception, [CanBeNull] object model);

        protected abstract ModelStateDictionary Handle([NotNull] Exception exception, [CanBeNull] object model);

        #endregion
    }
}