using System.Threading.Tasks;
using WebHook.Data.Models.POSRequestHandlers.Dtos;

namespace WebHook.Services.POSRequestHandlers
{
    public interface IPosRequestHandler
    {
        Task SendUpdateDeliveryRequest(UpdateDeliveryRequest input);
    }
}