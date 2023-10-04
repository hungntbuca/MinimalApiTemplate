using Quartz;
using System.Threading.Tasks;
using WebHook.Services.DeliveryInfoServices;

namespace WebHook.Services.BackgroundJob.UpdateDeliveryInfo
{
    public class UpdateDeliveryInfoJob : IJob
    {
        private readonly IDeliveryInfoServices _deliveryInfoServices;

        public UpdateDeliveryInfoJob(IDeliveryInfoServices deliveryInfoServices)
        {
            _deliveryInfoServices = deliveryInfoServices;
        }

        public async Task Execute(IJobExecutionContext context)
        {
            await _deliveryInfoServices.UpdateDeliveryInfoWork(false);
        }
    }
}
