namespace WebHook.Data.Models.POSRequestHandlers.Dtos
{
    public class LoginResultDto
    {
        public string AccessToken { get; set; }
        public int ExpireInSeconds { get; set; }
    }
}