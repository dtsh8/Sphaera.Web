using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using JetBrains.Annotations;
using Microsoft.Extensions.Configuration;
using Sphaera.Web.Server.Helpers;
using Sphaera.Web.Server.Models;
using Sphaera.Web.Server.Security;
using Sphaera.Web.Server.Extensions;

namespace Sphaera.Web.Services
{
    public interface IDispatchSvcService
    {
        Task<DispatchService[]> GetList(IEnumerable<long> serviceIds, string orgCode);

        Task<DispatchService[]> GetUserServices(string serviceIds, ClaimsPrincipal user);
    }

    [UsedImplicitly]
    public class DispatchSvcService : IDispatchSvcService
    {
        private const string DispatchServiceUri = "/api/v1/DispatchService/Get";

        private readonly WebApiProxy _webApiProxy;

        public DispatchSvcService([NotNull] IConfiguration config)
        {
            string serviceUri = config["DispatchService_URI"];
            _webApiProxy = new WebApiProxy(serviceUri);
        }

        public async Task<DispatchService[]> GetList(IEnumerable<long> serviceIds, string orgCode)
        {
            var request = new DispatchServiceRequest()
            {
                DispatchServiceIds = serviceIds?.ToArray(),
                OrgCode = orgCode
            };
            var dispatchServices = await _webApiProxy.PostAsync<DispatchServiceRequest, DispatchService[]>(DispatchServiceUri, request);
            return dispatchServices;
        }

        public async Task<DispatchService[]> GetUserServices(string serviceIds, ClaimsPrincipal user)
        {
            IEnumerable<long> dispatchServicesIds;
            if (user.IsInRole(Role.ChiefMunicipal) || user.IsInRole(Role.ChiefRegion))
            {
                dispatchServicesIds = user.GetDispatchServicesIds();
            }
            else
            {
                dispatchServicesIds = string.IsNullOrEmpty(serviceIds)
                    ? null
                    : serviceIds.Split('|', StringSplitOptions.RemoveEmptyEntries)
                        .Select(t => Convert.ToInt64(t))
                        .ToList();
            }

            string orgCode = null;
            if (user.IsInRole(Role.StaffAdmin) || user.IsInRole(Role.StaffDispatcher) || user.IsInRole(Role.StaffSupervisor))
            {
                orgCode = user.GetOrgCode();
            }

            return await GetList(dispatchServicesIds, orgCode);
        }
    }
}