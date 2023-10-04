namespace WebHook.WebHook.MinimalApi.Infracture;

using global::WebHook.Data.Common;
using global::WebHook.EntityFrameWork;
using global::WebHook.Services.DeliveryInfoServices;
using global::WebHook.Services.Hooks.GiaoHangTietKiem;
using global::WebHook.Services.POSRequestHandlers;
using global::WebHook.Services.Repositories;
using global::WebHook.Services.RequestHandlers;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

public static class DependencyInjection
{
    public static IServiceCollection AddDependencyInjection(this IServiceCollection services)
    {
        services.AddHttpClient();
        // Data repositories
        services.TryAddSingleton<IHttpContextAccessor, HttpContextAccessor>();
        services.AddScoped(typeof(IDeletableEntityRepository<>), typeof(EfDeletableEntityRepository<>));
        services.AddScoped(typeof(IRepository<>), typeof(EfRepository<>));
        services.AddScoped<IDbQueryRunner, DbQueryRunner>();

        //Request handeler
        services.AddScoped<IHttpClientRequestHandler, HttpClientRequestHandler>();
        services.AddScoped<IPosRequestHandler, PosRequestHandler>();
        services.AddScoped<IGiaoHangTietKiemHookAppService, GiaoHangTietKiemHookAppService>();
        services.AddScoped<IDeliveryInfoServices, DeliveryInfoServices>();
        return services;
    }
}
