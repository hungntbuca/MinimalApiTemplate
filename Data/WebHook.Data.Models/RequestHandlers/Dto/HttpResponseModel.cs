using System.Net;

namespace WebHook.Data.Models.RequestHandlers.Dto
{
    public class HttpResponseModel<T>
    {
        public int StatusCode { get; set; }
        public bool IsSucceed { get; set; }
        public string Message { get; set; }
        public T Data { get; set; }

        public ErrorResponseModel Error { get; set; }

        public static HttpResponseModel<T> Successed(T? data, HttpStatusCode statusCode = HttpStatusCode.OK)
        {
            return new HttpResponseModel<T>
            {
                IsSucceed = true,
                StatusCode = (int)statusCode,
                Data = data ?? default,
            };
        }

        public static HttpResponseModel<T> Failured(string message = "", HttpStatusCode statusCode = HttpStatusCode.UnprocessableEntity, ErrorResponseModel error = default)
        {
            return new HttpResponseModel<T>
            {
                IsSucceed = false,
                StatusCode = (int)statusCode,
                Message = message,
                Error = error
            };
        }
    }
}
