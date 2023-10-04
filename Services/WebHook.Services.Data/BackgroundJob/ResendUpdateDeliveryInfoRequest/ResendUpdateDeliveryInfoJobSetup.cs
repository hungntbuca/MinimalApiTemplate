using Microsoft.Extensions.Options;
using Quartz;
using WebHook.Data.Models.AppSettings;
using WebHook.Services.BackgroundJob.Common;
using WebHook.Services.Utilities;

namespace WebHook.Services.BackgroundJob.ResendUpdateDeliveryInfoRequest
{

    public class ResendUpdateDeliveryInfoJobSetup : IConfigureOptions<QuartzOptions>, IAppBackgroundJobSetup
    {
        private readonly UpdateDeliveryBackgroundJobSettings _jobSettings;

        public ResendUpdateDeliveryInfoJobSetup(IOptions<AppSettings> appSettings)
        {
            _jobSettings = appSettings.Value.UpdateDeliveryBackgroundJobSettings;
        }
        public void Configure(QuartzOptions options)
        {
            options.SetUpJobWithSimpleSchedule<ResendUpdateDeliveryInfoJob>(schedule => schedule.WithIntervalInMinutes(_jobSettings.ResendScheduleWithIntervalInMinutes).RepeatForever());
        }
    }
}
