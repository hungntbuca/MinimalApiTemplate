using System;
using System.Net.Http;
using System.Threading.Tasks;
using WebHook.Application.RequestHandlers;

namespace WebHook.Services.POSRequestHandlers
{
    public class POSRequestHandler
    {
        protected readonly IHttpClientFactory _httpClientFactory;
        protected readonly IHttpClientRequestHandler _httpClientRequestHandler;
        private readonly IMemoryCache _memoryCache;
        protected readonly string _baseUrl;
        private readonly string _tokenCacheKey = "POSTokenCacheKey";

        public POSRequestHandler(IHttpClientFactory httpClientFactory, IHttpClientRequestHandler httpClientFactoryService)
        {
            _httpClientFactory = httpClientFactory;
            _httpClientRequestHandler = httpClientFactoryService;

            // TODO: POS url
            _baseUrl = "https://services-staging.ghtklab.com";

            CreateHttpClient();
        }

        public async Task SendUpdateDeliveryRequest(UpdateDeliveryRequest input)
        {
            var _httpClient = await CreateHttpClient();
            await _httpClientRequestHandler.PostAsync<UpdateDeliveryRequest, object>(_httpClient, "/UpdateDeleverys", input);
        }

        private async Task<HttpClient> CreateHttpClient()
        {
            var httpClient = _httpClientFactory.CreateClient();
            httpClient.BaseAddress = new Uri(_baseUrl);
            var loginResult = _memoryCache.Get<LoginResultDto>(_tokenCacheKey);
            if (loginResult == null)
            {
                loginResult = await Login(httpClient);
                _memoryCache.Set(_tokenCacheKey, loginResult, TimeSpan.FromSeconds(loginResult.ExpireInSeconds));
            }
            httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer ", loginResult.AccessToken);
            return httpClient;

        }

        private async ValueTask<LoginResultDto> Login(HttpClient client)
        {
            var res = await _httpClientRequestHandler.PostAsync<LoginInputDto, LoginResultDto>(client, "/login-webhook", new LoginInputDto() { UserName = "admin", Password = "123qwe" });
            if (res?.IsSucceed != true || string.IsNullOrEmpty(res.Data?.AccessToken))
                throw new UnauthorizedAccessException("Đăng nhập thất bại");

            return res.Data;

        }

    }
}