using System.Collections.Generic;
using WebHook.Data.Models.DeliveryInfos.Dtos;

namespace WebHook.Data.Models.POSRequestHandlers.Dtos
{
    public class UpdateDeliveryRequest
    {
        public List<DeliveryInfoDto> Items { get; set; }
        public string RequestToken { get; set; }
    }
}