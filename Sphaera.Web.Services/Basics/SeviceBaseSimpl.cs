using System.Threading.Tasks;
using JetBrains.Annotations;
using Sphaera.Web.Server.Helpers;

namespace Sphaera.Web.Services.Basics
{
    // TODO Использовать один инстанс WebApiProxy а лучше Flurl https://aspnetmonsters.com/2016/08/2016-08-27-httpclientwrong/
    // HttpClient не используется. Используется HttpClientFactory. Именно потому, что HttpClient течет ресурсами и лочит сокеты.
    // Один инстанс не используется потому, что получение и возвращение сокетов в пул должно происходить в каждом вызове отдельно.
    // Иначе нужно будет делать CreateClient в каждом методе WebApiProxy, а это - неудобно.
    // Открыт к продолжению дискуссии :)
    public abstract class SeviceBaseSimple
    {
        #region Constructor

        protected SeviceBaseSimple()
        {
            SvcUrl = string.Empty;
        }

        #endregion

        #region Protected Properties

        [NotNull]
        protected string SvcUrl { get; set; }

        #endregion 

        #region Protected Methods

        protected async Task<T[]> GetList<T>(string url) where T: class
        {
            var proxy = new WebApiProxy(SvcUrl);
            return await proxy.GetResultAsync<T[]>(url);
        }

        protected async Task<T> Get<T>(string url) where T: class
        {
            var proxy = new WebApiProxy(SvcUrl);
            return await proxy.GetResultAsync<T>(url);
        }

        protected async Task<TResult> Set<T, TResult>(string url, T obj) where T: class
        {
            var proxy = new WebApiProxy(SvcUrl);
            return await proxy.PostAsync<T, TResult>(url, obj);
        }

        protected async Task Update<T>(string url, T obj) where T: class
        {
            var proxy = new WebApiProxy(SvcUrl);
            await proxy.PutRequestAsync(url, obj);
        }

        protected async Task<TResult> Update<T, TResult>(string url, T obj) where T: class
        {
            var proxy = new WebApiProxy(SvcUrl);
            return await proxy.PutAsync<T, TResult>(url, obj);
        }

        protected async Task<T[]> BulkUpdate<T>(string url, T[] data) where T: class
        {
            var proxy = new WebApiProxy(SvcUrl);
            return await proxy.PostRequestGetResultAsync(url, data);
        }

        protected async Task Delete(string url)
        {
            var proxy = new WebApiProxy(SvcUrl);
            await proxy.DeleteRequestAsync(url);
        }

        protected async Task<TResult> Delete<TResult>(string url)
        {
            var proxy = new WebApiProxy(SvcUrl);
            return await proxy.DeleteAsync<TResult>(url);
        }

        #endregion
    }
}