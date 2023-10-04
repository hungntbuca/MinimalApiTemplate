using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Options;
using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using WebHook.Data.Models.AppSettings;
using WebHook.Data.Models.POSRequestHandlers.Dtos;
using WebHook.Services.RequestHandlers;

namespace WebHook.Services.POSRequestHandlers
{
    public class PosRequestHandler : IPosRequestHandler
    {
        protected readonly IHttpClientFactory _httpClientFactory;
        protected readonly IHttpClientRequestHandler _httpClientRequestHandler;
        private readonly IMemoryCache _memoryCache;
        private readonly string _tokenCacheKey = "POSTokenCacheKey";
        private readonly PosRequestSettings _settings;

        public PosRequestHandler(
            IHttpClientFactory httpClientFactory,
            IHttpClientRequestHandler httpClientFactoryService,
            IMemoryCache memoryCache,
            IOptions<AppSettings> appSettings)
        {
            _httpClientFactory = httpClientFactory;
            _httpClientRequestHandler = httpClientFactoryService;
            _settings = appSettings.Value.PosRequestSettings;
            _memoryCache = memoryCache;
        }

        public async Task SendUpdateDeliveryRequest(UpdateDeliveryRequest input)
        {
            var _httpClient = await CreateHttpClient();
            await _httpClientRequestHandler.PostAsync<UpdateDeliveryRequest, object>(_httpClient, "/UpdateDeleverys", input);
        }

        private async Task<HttpClient> CreateHttpClient()
        {
            var httpClient = _httpClientFactory.CreateClient();
            httpClient.BaseAddress = new Uri(_settings.BaseUrl);

            var loginResult = _memoryCache.Get<LoginResultDto>(_tokenCacheKey);

            if (loginResult == null)
            {
                loginResult = await Login(httpClient);
                _memoryCache.Set(_tokenCacheKey, loginResult, TimeSpan.FromSeconds(loginResult.ExpireInSeconds));
            }

            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer ", loginResult.AccessToken);
            return httpClient;

        }

        private async ValueTask<LoginResultDto> Login(HttpClient client)
        {
            var res = await _httpClientRequestHandler.PostAsync<LoginInputDto, LoginResultDto>(client, "/login-webhook", new LoginInputDto() { UserName = _settings.User, Password = _settings.Password });
            if (res?.IsSucceed != true || string.IsNullOrEmpty(res.Data?.AccessToken))
                throw new UnauthorizedAccessException("Đăng nhập thất bại");

            return res.Data;

        }
    }
}