using System;
using JetBrains.Annotations;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace Sphaera.Web.Api.ExceptionHandlers.Interfaces
{
    public interface IExceptionHandlingService
    {
        [CanBeNull]
        ModelStateDictionary Handle([NotNull] Exception exception, [CanBeNull] object model);
    }
}