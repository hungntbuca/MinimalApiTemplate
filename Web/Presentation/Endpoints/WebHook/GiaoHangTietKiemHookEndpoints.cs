namespace WebHook.WebHook.MinimalApi.Endpoints.WebHook;

using global::WebHook.Data.Models.GiaoHangTietKiem.Dtos;
using global::WebHook.Services.Hooks.GiaoHangTietKiem;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

public static class GiaoHangTietKiemHookEndpoints
{
    public static WebApplication MapGiaoHangTietKiemWebHookEndpoints(this WebApplication app)
    {
        var root = app.MapGroup("Delivery/GiaoHangTietKiemWebHook")
            .WithTags("GiaoHangTietKiemWebHook")
            .WithOpenApi();

        _ = root.MapPost("/UpdateStatusDelivery", async ([FromBody] UpdateStatusDeliveryInput request, IGiaoHangTietKiemHookAppService giaoHangTietKiemHookAppService) =>
        {
            try
            {
                return Results.Ok(await giaoHangTietKiemHookAppService.UpdateStatusDelivery(request));
            }
            catch (Exception ex)
            {
                return Results.Problem(ex.StackTrace, ex.Message, StatusCodes.Status500InternalServerError);
            }
        })
        .Produces<BaseResponse>()
        .ProducesProblem(StatusCodes.Status404NotFound)
        .ProducesProblem(StatusCodes.Status500InternalServerError)
        .WithSummary("Update Delivery from GiaoHangTietKiem")
        .WithDescription("\n    Post /Delivery/GiaoHangTietKiemWebHook");

        return app;
    }
}
