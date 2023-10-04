using Quartz;
using System.Threading.Tasks;
using WebHook.Services.DeliveryInfoServices;

namespace WebHook.Services.BackgroundJob.ResendUpdateDeliveryInfoRequest
{

    public class ResendUpdateDeliveryInfoJob : IJob
    {
        private readonly IDeliveryInfoServices _deliveryInfoServices;

        public ResendUpdateDeliveryInfoJob(IDeliveryInfoServices deliveryInfoServices)
        {
            _deliveryInfoServices = deliveryInfoServices;
        }

        public async Task Execute(IJobExecutionContext context)
        {
            await _deliveryInfoServices.UpdateDeliveryInfoWork(true);
        }
    }
}
