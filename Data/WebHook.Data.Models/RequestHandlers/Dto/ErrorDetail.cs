namespace WebHook.Data.Models.RequestHandlers.Dto
{
    public class ErrorDetail
    {
        public string Code { get; set; }

        public string PartnerId { get; set; }

        public string GhtkLabel { get; set; }

        public string Created { get; set; }

        public int? Status { get; set; }
    }
}
