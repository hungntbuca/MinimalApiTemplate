using System.Threading.Tasks;
using WebHook.Data.Models.DeliveryInfos.Dtos;
using WebHook.Data.Models.GiaoHangTietKiem.Dtos;

namespace WebHook.Services.DeliveryInfoServices
{
    public interface IDeliveryInfoServices
    {
        /// <summary>
        /// Gửi request update trạng thái đơn hàng tới vipos 
        /// </summary>
        /// <returns></returns>
        ValueTask UpdateDeliveryInfoWork(bool isResend);

        /// <summary>
        /// Xóa bản ghi DeliveryInfoDto khi co request từ bên Vipos
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        ValueTask<BaseResponse> DeleteByPosRequest(DeliveryInfoDto input);
    }

}
