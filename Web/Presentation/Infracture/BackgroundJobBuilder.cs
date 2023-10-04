using FluentValidation;
using Quartz;
using System.Reflection;
using WebHook.Services.BackgroundJob.Common;

namespace WebHook.WebHook.MinimalApi.Infracture
{
    public static class BackgroundJobBuilder
    {
        /// <summary>
        /// RegisterBackgroundJob
        /// </summary>
        /// <param name="builder"></param>
        public static void RegisterBackgroundJob(this WebApplicationBuilder builder)
        {
            //All BackgroundJobSetup class need to be inherited from IAppBackgroundJobSetup and putted in same assembly to be register
            var appJobOptionType = typeof(IAppBackgroundJobSetup);
            var assembly = Assembly.GetAssembly(appJobOptionType);

            var optionTypes = assembly.GetTypes()
                .Where(type => appJobOptionType.IsAssignableFrom(type) && !type.IsInterface && !type.IsAbstract).ToList();

            foreach (var optionType in optionTypes)
            {
                builder.Services.ConfigureOptions(optionType);
            }
        }

        public static void BackgroundJobConfigure(this WebApplicationBuilder builder)
        {
            _ = builder.Services.AddQuartz(q =>
            {
                q.UseMicrosoftDependencyInjectionJobFactory();
            });

            // Add the Quartz.NET hosted service
            builder.Services.AddQuartzHostedService(q => q.WaitForJobsToComplete = true);

            //Register all background job
            builder.RegisterBackgroundJob();
        }
    }
}
