using System;
using System.Text.Json.Serialization;
using WebHook.Data.Common.Enum;

namespace WebHook.Data.Models.GiaoHangTietKiem.Dtos
{
    public class UpdateStatusDeliveryInput
    {
        [property: JsonPropertyName("partner_id")]
        public string PartnerId { get; set; }

        [property: JsonPropertyName("label_id")]
        public string LabelId { get; set; }

        [property: JsonPropertyName("status_id")]
        public DeliveryStatus? StatusId { get; set; }

        [property: JsonPropertyName("action_time")]
        public DateTime? ActionTime { get; set; }

        [property: JsonPropertyName("reason_code")]
        public ReasonCode? ReasonCode { get; set; }

        [property: JsonPropertyName("reason")]
        public string Reason { get; set; }

        [property: JsonPropertyName("weight")]
        public decimal? Weight { get; set; }

        [property: JsonPropertyName("fee")]
        public decimal? Fee { get; set; }

        [property: JsonPropertyName("pick_money")]
        public decimal? PickMoney { get; set; }

        [property: JsonPropertyName("return_part_package")]
        public int? ReturnPartPackage { get; set; }
    }
}
