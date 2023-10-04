using Quartz;
using System;

namespace WebHook.Services.Utilities
{
    public static class BackGroundJobUltilities
    {
        public static QuartzOptions SetUpJobWithSimpleSchedule<T>(this QuartzOptions options, Action<SimpleScheduleBuilder> action) where T : IJob
        {
            var jobKey = JobKey.Create(typeof(T).FullName);
            return options.AddJob<T>(jobBuilder => jobBuilder.WithIdentity(jobKey))
                          .AddTrigger(trigger => trigger.ForJob(jobKey).WithSimpleSchedule(action));
        }
    }
}
