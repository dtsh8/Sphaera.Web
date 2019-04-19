using Newtonsoft.Json;

namespace Sphaera.Web.Core.WebPush
{
    public class WebPushSubscriptionKeys
    {
        [JsonProperty("p256dh")]
        public string P256DH { get; set; }

        public string Auth { get; set; }
    }
}
