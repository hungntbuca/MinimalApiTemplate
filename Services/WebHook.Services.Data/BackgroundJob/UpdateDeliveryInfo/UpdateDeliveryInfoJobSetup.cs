using Microsoft.Extensions.Options;
using Quartz;
using WebHook.Data.Models.AppSettings;
using WebHook.Services.BackgroundJob.Common;
using WebHook.Services.Utilities;

namespace WebHook.Services.BackgroundJob.UpdateDeliveryInfo
{
    public class UpdateDeliveryInfoJobSetup : IConfigureOptions<QuartzOptions>, IAppBackgroundJobSetup
    {
        private readonly UpdateDeliveryBackgroundJobSettings _jobSettings;

        public UpdateDeliveryInfoJobSetup(IOptions<AppSettings> appSettings)
        {
            _jobSettings = appSettings.Value.UpdateDeliveryBackgroundJobSettings;
        }
        public void Configure(QuartzOptions options)
        {
            options.SetUpJobWithSimpleSchedule<UpdateDeliveryInfoJob>(schedule => schedule.WithIntervalInMinutes(_jobSettings.UpdateScheduleWithIntervalInMinutes).RepeatForever());
        }
    }
}
