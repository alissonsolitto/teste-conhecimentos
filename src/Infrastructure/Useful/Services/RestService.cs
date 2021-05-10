using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Useful.ActionResult;
using Useful.Exceptions;
using Useful.Interfaces;

namespace Useful.Services
{
    public class RestService : IRestService
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public RestService(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        /// <summary>
        /// Método para eftuar requisições do tipo GET
        /// </summary>
        /// <typeparam name="TData"></typeparam>
        /// <param name="baseUrl"></param>
        /// <param name="resource"></param>
        /// <param name="valuePairs"></param>
        /// <returns></returns>
        public async Task<WebApiResultModel<TData>> GetQueryAsync<TData>(string baseUrl, string resource, IDictionary<string, string> valuePairs = null)
        {
            var httpClient = CreateHttpClient(baseUrl);
            var requestUri = resource;

            //Dictionary to parameters query
            if (valuePairs != null)
            {
                var queryParameters = string.Join("&", valuePairs.Select(x => $"{x.Key}={x.Value}"));
                requestUri = $"{resource}?{queryParameters}";
            }

            var response = await httpClient.GetAsync(requestUri);
            response.EnsureSuccessStatusCode();

            var data = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<WebApiResultModel<TData>>(data, new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore });
        }

        public Task<WebApiResultModel<TData>> PostAsync<TData>(string baseUrl, string resource, object data)
        {
            throw new NotImplementedException();
        }

        public Task<WebApiResultModel<TData>> PutAsync<TData>(string baseUrl, string resource, object data)
        {
            throw new NotImplementedException();
        }

        public Task<WebApiResultModel<TData>> DeleteAsync<TData>(string baseUrl, string resource, object data)
        {
            throw new NotImplementedException();
        }

        private HttpClient CreateHttpClient(string baseUrl)
        {
            var httpClient = _httpClientFactory.CreateClient();
            httpClient.BaseAddress = new Uri(baseUrl);
            httpClient.DefaultRequestHeaders.Accept.Clear();
            httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            return httpClient;
        }
    }
}
