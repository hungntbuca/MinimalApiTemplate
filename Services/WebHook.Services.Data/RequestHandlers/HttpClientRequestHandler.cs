using System.Net;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading;
using System.Threading.Tasks;
using WebHook.Data.Models.RequestHandlers.Dto;
using WebHook.Data.Models.RequestHandlers.Dto.Base;
using WebHook.Services.RequestHandlers.Base;

namespace WebHook.Services.RequestHandlers
{
    public class HttpClientRequestHandler : HttpClientRequestBase, IHttpClientRequestHandler
    {
        public async Task<HttpResponseModel<TResponse>> GetAsync<TRequest, TResponse>(HttpClient httpClient, string url, TRequest input, CancellationToken cancellationToken = default)
        {
            var httpResponse = await httpClient.GetAsync(ParseToQueryString(url, input), cancellationToken);

            return await ParseResponseData<TResponse>(httpResponse, message: await GetErrorResponse(httpResponse));
        }

        public async Task<HttpResponseModel<TResponse>> PostAsync<TRequest, TResponse>(HttpClient httpClient, string url, TRequest input, CancellationToken cancellationToken = default)
        {
            var httpResponse = await httpClient.PostAsync(url, content: ParseStringContent(input), cancellationToken);

            return await ParseResponseData<TResponse>(httpResponse, message: await GetErrorResponse(httpResponse));
        }

        public async Task<HttpResponseModel<TResponse>> PutAsync<TRequest, TResponse>(HttpClient httpClient, string url, TRequest input, CancellationToken cancellationToken = default)
        {
            var httpResponse = await httpClient.PutAsync(url, content: ParseStringContent(input), cancellationToken);

            return await ParseResponseData<TResponse>(httpResponse, message: await GetErrorResponse(httpResponse));
        }

        public async Task<HttpResponseModel<TResponse>> DeleteAsync<TRequest, TResponse>(HttpClient httpClient, string url, TRequest input, CancellationToken cancellationToken = default)
        {
            var httpResponse = await httpClient.DeleteAsync(ParseToQueryString(url, input), cancellationToken);

            return await ParseResponseData<TResponse>(httpResponse, message: await GetErrorResponse(httpResponse));
        }

        private async ValueTask<string> GetErrorResponse(HttpResponseMessage httpResponse)
        {
            if (httpResponse.StatusCode == HttpStatusCode.OK)
                return "";

            var errorResponse = await httpResponse.Content.ReadFromJsonAsync<ErrorFormUrlResponseModel>();
            return $"{errorResponse?.Title}: {errorResponse?.Description}";
        }
    }
}
