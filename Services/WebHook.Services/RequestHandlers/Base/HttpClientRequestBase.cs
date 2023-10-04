using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace WebHook.Services.RequestHandlers.Base
{
    public class HttpClientRequestBase
    {
        protected readonly JsonSerializerOptions _options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };

        protected virtual async Task<HttpResponseModel<TResponse>> ParseResponseData<TResponse>(HttpResponseMessage httpResponse)
        {
            var stream = await httpResponse.Content.ReadAsStreamAsync();
            if (httpResponse.StatusCode == HttpStatusCode.OK)
            {
                var data = await JsonSerializer.DeserializeAsync<TResponse>(stream, _options);
                return HttpResponseModel<TResponse>.Successed(data);
            }

            var response = await JsonSerializer.DeserializeAsync<ErrorResponseModel>(stream, _options);
            return HttpResponseModel<TResponse>.Failured(response?.Message, httpResponse.StatusCode, response);
        }
        protected virtual async Task<HttpResponseModel<TResponse>> ParseResponseData<TResponse>(HttpResponseMessage httpResponse, string message = "")
        {
            TResponse? data = default;
            if (httpResponse.StatusCode == HttpStatusCode.OK)
            {
                var stream = await httpResponse.Content.ReadAsStreamAsync();
                data = await JsonSerializer.DeserializeAsync<TResponse>(stream, _options);
            }

            return new HttpResponseModel<TResponse>
            {
                IsSucceed = httpResponse.StatusCode == HttpStatusCode.OK,
                StatusCode = (int)httpResponse.StatusCode,
                Message = message,
                Data = data
            };
        }

        protected virtual StringContent ParseStringContent<TRequest>(TRequest input)
        {
            var bodyRequest = new StringContent("");
            if (input != null)
            {
                bodyRequest = new StringContent(JsonSerializer.Serialize(input, _options), encoding: Encoding.UTF8, mediaType: "application/json");
            }

            return bodyRequest;
        }

        protected virtual FormUrlEncodedContent ParseFormUrlEncodedContent<TRequest>(TRequest input)
        {
            return input is not null
                    ? input.ConvertFromJson()
                    : new FormUrlEncodedContent(new Dictionary<string, string>());
        }

        protected virtual string ParseToQueryString<TRequest>(string url, TRequest input)
        {
            if (input is not null)
            {
                url = $"{url}?{input.ParseObjectToQueryString()}";
            }

            return url;
        }
    }
}
