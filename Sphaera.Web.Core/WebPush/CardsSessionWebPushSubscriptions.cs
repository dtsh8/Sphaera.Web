using System.Collections.Concurrent;
using System.Collections.Generic;

namespace Sphaera.Web.Core.WebPush
{
    public class CardsSessionWebPushSubscriptions<TKey, TValue>
    {
        public string SessionId { get; set; }

        public ConcurrentDictionary<TKey, TValue> WebPushSubscriptions { get; }

        public CardsSessionWebPushSubscriptions()
        {
            WebPushSubscriptions = new ConcurrentDictionary<TKey, TValue>();
        }

        public CardsSessionWebPushSubscriptions(IEnumerable<KeyValuePair<TKey, TValue>> items)
        {
            WebPushSubscriptions = new ConcurrentDictionary<TKey, TValue>(items);
        }

        public void RemoveSubscription(TKey key)
        {
            WebPushSubscriptions.TryRemove(key, out TValue val);
        }

        public void AddSubscription(TKey key, TValue value)
        {
            WebPushSubscriptions.AddOrUpdate(key, value, (k, v) => value);
        }
    }
}
