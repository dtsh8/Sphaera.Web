using System;
using System.ServiceModel;
using JetBrains.Annotations;

namespace Sphaera.Web.Api.ExceptionHandlers.Handlers
{
    [UsedImplicitly]
    public class CommunicationExceptionHandler : DefaultExceptionHandler
    {
        #region Constructor

        public CommunicationExceptionHandler()
        {
            Order = 10;
        }

        #endregion

        #region Protected members

        protected override bool CanHandle(Exception exception, object model)
        {
            return exception is CommunicationException;
        }

        #endregion
    }
}