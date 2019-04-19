using JetBrains.Annotations;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Sphaera.Web.Api.ExceptionHandlers.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Sphaera.Web.Api.Extensions
{
    public static class ExceptionHandlingServiceExtensions
    {
        public static bool TryHandle(
            [NotNull] this IExceptionHandlingService service,
            [NotNull] ModelStateDictionary modelState,
            [NotNull] Exception exception,
            [CanBeNull] object model)
        {
            if (service == null)
                throw new ArgumentNullException(nameof(service));

            if (modelState == null)
                throw new ArgumentNullException(nameof(modelState));

            if (exception == null)
                throw new ArgumentNullException(nameof(exception));

            var state = service.Handle(exception, model);
            if (state == null)
                return false;

            modelState.Merge(state);

            return true;
        }

        public static string TryHandle(
            [NotNull] this IExceptionHandlingService service,
            [NotNull] Exception exception)
        {
            if (service == null)
                throw new ArgumentNullException(nameof(service));

            if (exception == null)
                throw new ArgumentNullException(nameof(exception));

            try
            {
                var state = service.Handle(exception, null);
                if (state == null)
                    return string.Empty;

                return state.Aggregate(string.Empty, (current1, pair) =>
                                                     pair.Value.Errors.Aggregate(current1, (current, error) =>
                                                                                           current +
                                                                                           string.Concat(error.ErrorMessage, "\r\n")));
            }
            catch (NullReferenceException)
            {
            }
            catch (ArgumentNullException)
            {
            }

            return string.Empty;
        }
    }
}
