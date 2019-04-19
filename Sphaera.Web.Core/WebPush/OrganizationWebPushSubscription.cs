using System;
using Newtonsoft.Json;

namespace Sphaera.Web.Core.WebPush
{
    public class OrganizationWebPushSubscription
    {
        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("endpoint")]
        public string Endpoint { get; set; }

        [JsonProperty("p256dh")]
        public string P256dh { get; set; }

        [JsonProperty("auth")]
        public string Auth { get; set; }

        /// <summary>
        /// Код организации, к которой относится диспетчер.
        /// </summary>
        [JsonProperty("organizationCode")]
        public string OrganizationCode { get; set; }

        [JsonProperty("expireTime")]
        public DateTimeOffset ExpireTime { get; set; }
    }
}
