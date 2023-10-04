using WebHook.WebHook.MinimalApi.Endpoints.DeliveryInfo;
using WebHook.WebHook.MinimalApi.Endpoints.WebHook;

namespace WebHook.WebHook.MinimalApi.Endpoints.Root
{
    public static class RootEndpoint
    {
        public static WebApplication MapAllEndpoints(this WebApplication app)
        {
            _ = app.MapGiaoHangTietKiemWebHookEndpoints();
            _ = app.MapDeliveryInfoEndpoints();
            return app;
        }
    }
}
