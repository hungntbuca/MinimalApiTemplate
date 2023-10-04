using System.Threading.Tasks;
using WebHook.Data.Models.GiaoHangTietKiem.Dtos;

namespace WebHook.Services.Hooks.GiaoHangTietKiem
{
    public interface IGiaoHangTietKiemHookAppService
    {
        Task<BaseResponse> UpdateStatusDelivery(UpdateStatusDeliveryInput input);
    }
}
