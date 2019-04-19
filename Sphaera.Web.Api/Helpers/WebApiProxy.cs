using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Security.Authentication;
using System.Threading.Tasks;
using Autofac;
using JetBrains.Annotations;
using Newtonsoft.Json;
using Sphaera.Web.Api.ExceptionHandlers.Exceptions;
using Sphaera.Web.Api.Extensions;
using Sphaera.Web.Api.Resources;

namespace Sphaera.Web.Api.Helpers
{
    public sealed class WebApiProxy : IDisposable
    {
	    public const string TrustAnyCertificateClient = "TrustAnyCertificateClient";
	    
        #region Private Fields

        [NotNull]
        private readonly HttpClient _client;

        #endregion

        #region Constructor

        public WebApiProxy([NotNull] string serviceAddress, bool trustAnyCertificate = false)
        {
            if(string.IsNullOrEmpty(serviceAddress))
                throw new ArgumentNullException(nameof(serviceAddress), string.Format(Errors.NullNotAllowed, nameof(serviceAddress)));

            var clientFactory = Startup.Container.Resolve<IHttpClientFactory>();
	        if (trustAnyCertificate)
	        {
		        _client = clientFactory.CreateClient(TrustAnyCertificateClient);
	        }
	        else
	        {
		        _client = clientFactory.CreateClient();
	        }
            if(_client == null)
                throw new InvalidOperationException(Errors.ClientCreationError);

            _client.BaseAddress = new Uri(serviceAddress);

            _client.DefaultRequestHeaders.Accept.Clear();
            _client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }

        #endregion

        #region Public Methods

        [NotNull]
        public async Task<T> GetResultAsync<T>(string url) where T : class
        {
            var task = _client.GetAsync(url).ContinueWith(taskwithresponse =>
            {
                var response = taskwithresponse.Result;
                if (!response.IsSuccessStatusCode)
                    throw GetApiException(response);

                var jsonString = response.Content.ReadAsStringAsync();
                jsonString.Wait();

                if(string.IsNullOrEmpty(jsonString.Result))
                    throw new ApiModelException(Errors.EmptyResult);

                return JsonConvert.DeserializeObject<T>(jsonString.Result);
            });

            return await task;
        }

        [NotNull]
        public async Task<T> PostRequestGetResultAsync<T>(string url, T obj)
        {
            return await PostAsync<T, T>(url, obj);
        }

        [NotNull]
        public async Task<TResult> PostAsync<T, TResult>(string url, T obj)
        {
            var httpResponseMessage = await _client.PostAsJsonAsync(url, obj);
            return await GetResultFromResponseMessage<TResult>(httpResponseMessage);
        }

        public async Task PostRequestAsync<T>(string url, T obj) where T : class
        {
            await _client.PostAsJsonAsync(url, obj).ContinueWith(taskwithresponse =>
            {
                var response = taskwithresponse.Result;
                if (!response.IsSuccessStatusCode)
                    throw GetApiException(response);
            });
        }

        public async Task<TResult> PostRequestAsync<TResult>(string url)
        {
            var httpResponseMessage = await _client.PostAsync(url, new StringContent(string.Empty));
            return await GetResultFromResponseMessage<TResult>(httpResponseMessage);
        }

        public async Task PutRequestAsync<T>(string url, T obj) where T : class
        {
            await _client.PutAsJsonAsync(url, obj).ContinueWith(taskwithresponse =>
            {
                var response = taskwithresponse.Result;
                if (!response.IsSuccessStatusCode)
                    throw GetApiException(response);
            });
        }

        [NotNull]
        public async Task<T> PutAsync<T>(string url, T obj) where T : class
        {
            return await PutAsync<T, T>(url, obj);
        }

        [NotNull]
        public async Task<TResult> PutAsync<T, TResult>(string url, T obj) where T : class
        {
            var httpResponseMessage = await _client.PutAsJsonAsync(url, obj);
            return await GetResultFromResponseMessage<TResult>(httpResponseMessage);
        }

        public async Task DeleteRequestAsync(string url)
        {
            await _client.DeleteAsync(url).ContinueWith(taskwithresponse =>
            {
                var response = taskwithresponse.Result;
                if (!response.IsSuccessStatusCode)
                    throw GetApiException(response);
            });
        }

        public async Task<TResult> DeleteAsync<TResult>(string url)
        {
            var httpResponseMessage = await _client.DeleteAsync(url);
            return await GetResultFromResponseMessage<TResult>(httpResponseMessage);
        }

        [CanBeNull]
        public T GetAsyncResult<T>(string url) where T : class
        {
            T result = null;

            var task = _client.GetAsync(url).ContinueWith(taskwithresponse =>
            {
                var response = taskwithresponse.Result;
                if (response.IsSuccessStatusCode)
                {
                    var jsonString = response.Content.ReadAsStringAsync();
                    jsonString.Wait();

                    result = JsonConvert.DeserializeObject<T>(jsonString.Result);
                }
                else
                    throw GetApiException(response);
            });

            task.Wait();

            return result;
        }

        public void PutAsyncRequest<T>(string url, T obj) where T : class
		{
			var task = _client.PutAsJsonAsync(url, obj).ContinueWith(taskwithresponse =>
			{
				var response = taskwithresponse.Result;
				if (!response.IsSuccessStatusCode)
					throw GetApiException(response);
			});

			task.Wait();
		}

        public void PutAsyncRequest(string url)
		{
			var task = _client.PutAsync(url, new StringContent(string.Empty)).ContinueWith(taskwithresponse =>
			{
				var response = taskwithresponse.Result;
				if (!response.IsSuccessStatusCode)
					throw GetApiException(response);
			});

			task.Wait();
		}

		[CanBeNull]
		public T PutAsyncRequestGetResult<T>(string url, T obj) where T : class
		{
			T result = null;

			var task = _client.PutAsJsonAsync(url, obj).ContinueWith(taskwithresponse =>
			{
				var response = taskwithresponse.Result;
				if (response.IsSuccessStatusCode)
				{
					var jsonString = response.Content.ReadAsStringAsync();
					jsonString.Wait();

					result = JsonConvert.DeserializeObject<T>(jsonString.Result);
				}
				else
					throw GetApiException(response);
			});

			task.Wait();

			return result;
		}

		[CanBeNull]
		public T PutAsyncRequestGetResult<T>(string url) where T : class
		{
			T result = null;

			var task = _client.PutAsync(url, new StringContent(string.Empty)).ContinueWith(taskwithresponse =>
			{
				var response = taskwithresponse.Result;
				if (response.IsSuccessStatusCode)
				{
					var jsonString = response.Content.ReadAsStringAsync();
					jsonString.Wait();

					result = JsonConvert.DeserializeObject<T>(jsonString.Result);
				}
				else
					throw GetApiException(response);
			});

			task.Wait();

			return result;
		}

        public void PostAsyncRequest<T>(string url, T obj) where T : class
		{
			var task = _client.PostAsJsonAsync(url, obj).ContinueWith(taskwithresponse =>
			{
				var response = taskwithresponse.Result;
				if (!response.IsSuccessStatusCode)
					throw GetApiException(response);
			});

			task.Wait();
		}

        public void PostAsyncRequest(string url)
		{
			var task = _client.PostAsync(url, new StringContent(string.Empty)).ContinueWith(taskwithresponse =>
			{
				var response = taskwithresponse.Result;
				if (!response.IsSuccessStatusCode)
					throw GetApiException(response);
			});

			task.Wait();
		}

		[CanBeNull]
		public T PostAsyncRequestGetResult<T>(string url, T obj) where T : class
		{
			T result = null;

			var task = _client.PostAsJsonAsync(url, obj).ContinueWith(taskwithresponse =>
			{
				var response = taskwithresponse.Result;
				if (response.IsSuccessStatusCode)
				{
					var jsonString = response.Content.ReadAsStringAsync();
					jsonString.Wait();

					result = JsonConvert.DeserializeObject<T>(jsonString.Result);
				}
				else
					throw GetApiException(response);
			});

			task.Wait();

			return result;
		}

		[CanBeNull]
		public T PostAsyncRequestGetResult<T>(string url) where T : class
		{
			T result = null;

			var task = _client.PostAsync(url, new StringContent(string.Empty)).ContinueWith(taskwithresponse =>
			{
				var response = taskwithresponse.Result;
				if (response.IsSuccessStatusCode)
				{
					var jsonString = response.Content.ReadAsStringAsync();
					jsonString.Wait();

					result = JsonConvert.DeserializeObject<T>(jsonString.Result);
				}
				else
					throw GetApiException(response);
			});

			task.Wait();

			return result;
		}

        public void DelAsyncRequest(string url)
		{
			var task = _client.DeleteAsync(url).ContinueWith(taskwithresponse =>
			{
				var response = taskwithresponse.Result;
				if (!response.IsSuccessStatusCode)
					throw GetApiException(response);
			});

			task.Wait();
		}

        #endregion

        #region Implementation of IDisposable

        public void Dispose()
        {
            _client.Dispose();
        }

        #endregion

        #region Private Methods

        private static ApiModelException GetApiException(HttpResponseMessage response)
        {
            if (response.StatusCode == HttpStatusCode.Unauthorized)
                throw new AuthenticationException(Errors.Forbidden);

            var ex = new ApiModelException(string.Format(Errors.WebApiError, (int)response.StatusCode, response.ReasonPhrase, response.RequestMessage.RequestUri), response.StatusCode);

            try
            {
                var httpResult = response.Content.ReadAsStringAsync().Result;

                var result = JsonConvert.DeserializeObject<Dictionary<string, string[]>>(httpResult);
                if (result != null)
                {
                    string message = null;

                    if (result.ContainsKey(string.Empty))
                    {
                        message = string.Join(". ", result[string.Empty]);
                        result.Remove(string.Empty);
                    }

                    if (!string.IsNullOrEmpty(message))
                        ex = new ApiModelException(string.Format(Errors.WebApiError, (int)response.StatusCode, message, response.RequestMessage.RequestUri), response.StatusCode);

                    foreach (var err in result)
                        ex.Data.Add(err.Key, string.Join(". ", err.Value));
                }
            }
            catch
            {
                ex.Data.Add("StatusCode", response.StatusCode.ToString());
            }

            return ex;
        }

        private static async Task<TResult> GetResultFromResponseMessage<TResult>(HttpResponseMessage httpResponseMessage)
        {
            if (!httpResponseMessage.IsSuccessStatusCode)
            {
                throw GetApiException(httpResponseMessage);
            }

            var jsonString = await httpResponseMessage.Content.ReadAsStringAsync();
            if (string.IsNullOrEmpty(jsonString))
            {
                throw new ApiModelException(Errors.EmptyResult);
            }

            return JsonConvert.DeserializeObject<TResult>(jsonString);
        }

        #endregion
    }
}