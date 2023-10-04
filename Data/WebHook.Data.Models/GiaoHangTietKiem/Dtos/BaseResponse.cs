
using System.Text.Json.Serialization;

namespace WebHook.Data.Models.GiaoHangTietKiem.Dtos
{
    public class BaseResponse
    {
        [property: JsonPropertyName("success")]
        public bool Success { get; set; }

        [property: JsonPropertyName("message")]
        public string Message { get; set; }
    }
}
