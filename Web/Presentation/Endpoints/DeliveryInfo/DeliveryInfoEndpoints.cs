namespace WebHook.WebHook.MinimalApi.Endpoints.DeliveryInfo;

using global::WebHook.Data.Models.DeliveryInfos.Dtos;
using global::WebHook.Data.Models.GiaoHangTietKiem.Dtos;
using global::WebHook.Services.DeliveryInfoServices;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

public static class DeliveryInfoEndpoints
{
    public static WebApplication MapDeliveryInfoEndpoints(this WebApplication app)
    {
        var root = app.MapGroup("Delivery/DeliveryInfos")
            .WithTags("DeliveryInfos")
            .WithOpenApi();

        _ = root.MapPost("/ConfirmUpdatedRequest", async ([FromBody] DeliveryInfoDto request, IDeliveryInfoServices deliveryInfoServices) =>
        {
            try
            {
                return Results.Ok(await deliveryInfoServices.DeleteByPosRequest(request));
            }
            catch (Exception ex)
            {
                return Results.Problem(ex.StackTrace, ex.Message, StatusCodes.Status500InternalServerError);
            }
        })
        .Produces<BaseResponse>()
        .ProducesProblem(StatusCodes.Status404NotFound)
        .ProducesProblem(StatusCodes.Status500InternalServerError)
        .WithSummary("Confirm recieved request to update from Vipos")
        .WithDescription("\n    Post /Delivery/DeliveryInfos");

        return app;
    }
}
