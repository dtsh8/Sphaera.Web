using System;
using System.Collections.Generic;
using System.Linq;
using JetBrains.Annotations;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Sphaera.Web.Api.ExceptionHandlers.Interfaces;

namespace Sphaera.Web.Api.ExceptionHandlers
{
    [UsedImplicitly]
    public class ExceptionHandlingService : IExceptionHandlingService
    {
        #region Private fields

        private readonly IExceptionHandler _head;

        #endregion

        #region Constructor

        public ExceptionHandlingService([NotNull] IEnumerable<IExceptionHandler> handlers)
        {
            if (handlers == null)
                throw new ArgumentNullException(nameof(handlers));

            IExceptionHandler previous = null;
            foreach (var handler in handlers.OrderByDescending(x => x.Order))
            {
                handler.Initialize(previous);
                previous = handler;
            }

            _head = previous;
        }

        #endregion

        #region Public members

        public ModelStateDictionary Handle(Exception exception, object model)
        {
            return _head?.Handle(exception, model);
        }

        #endregion
    }
}