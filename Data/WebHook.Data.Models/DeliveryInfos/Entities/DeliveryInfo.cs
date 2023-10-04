using System;
using WebHook.Data.Common.Enum;
using WebHook.Data.Common.Models;

namespace WebHook.Data.Models.DeliveryInfos.Entities
{
    public class DeliveryInfo : BaseEntity<int>
    {
        public DeliveryInfo(string posDeliveryCode)
        {
            TenantId = GetTenantId(posDeliveryCode);
        }

        public DeliveryInfo()
        {

        }
        public int? TenantId { get; set; }
        public string POSCode { get; set; }
        public string DeliveryCode { get; set; }
        public string DataJson { get; set; }
        public string RequestToken { get; set; }
        public DateTime? SendRequestDate { get; set; }
        public int ResendCount { get; set; }
        public DeliveryPartnerType DeliveryPartnerType { get; set; }

        public int? GetTenantId(string posDeliveryCode)
        {
            var splitChars = posDeliveryCode?.Split(".");
            var tenantIdString = splitChars?.Length > 1 ? splitChars[splitChars.Length - 1] : null;
            int.TryParse(tenantIdString, out int tenantId);
            return tenantId > 0 ? tenantId : null;
        }
    }
}