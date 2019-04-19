using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Sphaera.Web.Core;

namespace Sphaera.Web.Api.Services
{
    public interface IIncidentStateService
    {
        Task<IncidentState> GetNew();
        Task<IncidentState> GetAccepted();
    }
    public class IncidentStateService : IIncidentStateService
    {
        public Task<IncidentState> GetAccepted()
        {
            throw new NotImplementedException();
        }

        public Task<IncidentState> GetNew()
        {
            throw new NotImplementedException();
        }
    }
}
