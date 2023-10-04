using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using WebHook.Data.Models.RequestHandlers.Dto;

namespace WebHook.Services.RequestHandlers
{
    public interface IHttpClientRequestHandler
    {
        Task<HttpResponseModel<TResponse>> GetAsync<TRequest, TResponse>(HttpClient httpClient, string url, TRequest input, CancellationToken cancellationToken = default);
        Task<HttpResponseModel<TResponse>> PostAsync<TRequest, TResponse>(HttpClient httpClient, string url, TRequest input, CancellationToken cancellationToken = default);
        Task<HttpResponseModel<TResponse>> PutAsync<TRequest, TResponse>(HttpClient httpClient, string url, TRequest input, CancellationToken cancellationToken = default);
        Task<HttpResponseModel<TResponse>> DeleteAsync<TRequest, TResponse>(HttpClient httpClient, string url, TRequest input, CancellationToken cancellationToken = default);
    }
}
