using System.Collections.Generic;
using System.Threading.Tasks;
using JetBrains.Annotations;
using Microsoft.Extensions.Configuration;
using Sphaera.Web.Server.Helpers;
using Sphaera.Web.Server.Models.WebPush;

namespace Sphaera.Web.Services
{
    public interface IWebPushSubscriptionService
    {
        Task<ICollection<OrganizationWebPushSubscription>> GetOrganizationsSubscriptions();

        Task<OrganizationWebPushSubscription> SaveOrganizationSubscription(OrganizationWebPushSubscription organizationWebPushSubscription);

        Task<bool> DeleteSubscription(string endpoint);
    }

    [UsedImplicitly]
    public class WebPushSubscriptionService : IWebPushSubscriptionService
    {
        private const string GetOrganizationsSubscriptionsUri = "/api/OrganizationWebPush/GetSubscriptions";

        private const string SaveOrganizationSubscriptionUri = "/api/OrganizationWebPush/SaveSubscription";

        private const string DeleteOrganizationSubscriptionByEndpointUri = "/api/OrganizationWebPush/DeleteSubscriptionByEndpoint";

        private readonly WebApiProxy _idmProxy;

        public WebPushSubscriptionService([NotNull] IConfiguration config)
        {
            var idmUrl = config["IdmInternalBaseAddress"];
            _idmProxy = new WebApiProxy(idmUrl, true);
        }

        public async Task<ICollection<OrganizationWebPushSubscription>> GetOrganizationsSubscriptions()
        {
            return await _idmProxy.GetResultAsync<ICollection<OrganizationWebPushSubscription>>(GetOrganizationsSubscriptionsUri);
        }

        public async Task<OrganizationWebPushSubscription> SaveOrganizationSubscription(OrganizationWebPushSubscription subscription)
        {
            return await _idmProxy.PostRequestGetResultAsync(SaveOrganizationSubscriptionUri, subscription);
        }

        public async Task<bool> DeleteSubscription(string endpoint)
        {
            return await _idmProxy.PostAsync<string, bool>(DeleteOrganizationSubscriptionByEndpointUri, endpoint);
        }
    }
}