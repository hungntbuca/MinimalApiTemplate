namespace WebHook.Data.Models.DeliveryInfos.Dtos
{
    public class DeliveryInfoDto
    {
        public int Id { get; set; }
        public int? TenantId { get; set; }
        public string POSCode { get; set; }
        public string DeliveryCode { get; set; }
        public string DataJson { get; set; }
        public string RequestToken { get; set; }
    }
}