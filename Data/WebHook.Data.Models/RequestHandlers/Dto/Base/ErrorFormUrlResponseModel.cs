namespace WebHook.Data.Models.RequestHandlers.Dto.Base
{
    public class ErrorFormUrlResponseModel
    {
        public string Description { get; set; }

        public string Title { get; set; }

        public string Code { get; set; }

        public int HttpCode { get; set; }
    }
}
