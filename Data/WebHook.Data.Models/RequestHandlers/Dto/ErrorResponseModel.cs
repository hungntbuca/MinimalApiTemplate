namespace WebHook.Data.Models.RequestHandlers.Dto
{
    public class ErrorResponseModel
    {
        public bool Success { get; set; }

        public string Message { get; set; }

        public string ErrorCode { get; set; }

        public string LogId { get; set; }

        public ErrorDetail Error { get; set; }
    }
}
