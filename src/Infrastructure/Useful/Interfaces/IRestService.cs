using System.Collections.Generic;
using System.Threading.Tasks;
using Useful.ActionResult;

namespace Useful.Interfaces
{
    public interface IRestService
    {
        public Task<WebApiResultModel<TData>> GetQueryAsync<TData>(string baseUrl, string resource, IDictionary<string, string> valuePairs = null);
        public Task<WebApiResultModel<TData>> PostAsync<TData>(string baseUrl, string resource, object data);
        public Task<WebApiResultModel<TData>> PutAsync<TData>(string baseUrl, string resource, object data);
        public Task<WebApiResultModel<TData>> DeleteAsync<TData>(string baseUrl, string resource, object data);
    }
}
