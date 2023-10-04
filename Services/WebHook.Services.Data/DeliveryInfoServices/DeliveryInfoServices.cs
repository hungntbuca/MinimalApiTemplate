using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using System;
using System.Linq;
using System.Threading.Tasks;
using WebHook.Data.Models.AppSettings;
using WebHook.Data.Models.DeliveryInfos.Dtos;
using WebHook.Data.Models.DeliveryInfos.Entities;
using WebHook.Data.Models.GiaoHangTietKiem.Dtos;
using WebHook.Data.Models.POSRequestHandlers.Dtos;
using WebHook.EntityFrameWork.Ultilities;
using WebHook.Services.Mapping;
using WebHook.Services.POSRequestHandlers;
using WebHook.Services.Repositories;

namespace WebHook.Services.DeliveryInfoServices
{
    public class DeliveryInfoServices : IDeliveryInfoServices
    {
        private readonly IRepository<DeliveryInfo> _deliveryInfoRepository;
        private readonly IPosRequestHandler _posRequestHandler;
        private readonly UpdateDeliveryBackgroundJobSettings _jobSettings;

        public DeliveryInfoServices(IRepository<DeliveryInfo> deliveryInfoRepository, IPosRequestHandler posRequestHandler, IOptions<AppSettings> appSettings)
        {
            _deliveryInfoRepository = deliveryInfoRepository;
            _posRequestHandler = posRequestHandler;
            _jobSettings = appSettings.Value.UpdateDeliveryBackgroundJobSettings;
        }

        /// <summary>
        /// Gửi request update trạng thái đơn hàng tới vipos
        /// </summary>
        /// <returns></returns>
        public async ValueTask UpdateDeliveryInfoWork(bool isResend)
        {
            var updatingItems = await _deliveryInfoRepository.GetAll()
                       .WhereIf(isResend, x => !string.IsNullOrEmpty(x.RequestToken))
                       .WhereIf(!isResend, x => string.IsNullOrEmpty(x.RequestToken))
                       .Take(_jobSettings.MaxItemsPerRequest)
                       .ToListAsync().ConfigureAwait(false);
            try
            {
                if (updatingItems?.Count > 0)
                {
                    //Token để verify
                    var requestToken = Guid.NewGuid().ToString();

                    foreach (var item in updatingItems)
                    {
                        item.SendRequestDate = DateTime.Now;
                        ++item.ResendCount;
                        item.RequestToken = requestToken;
                    }

                    await _deliveryInfoRepository.BulkUpdateAsync(updatingItems).ConfigureAwait(false);

                    //Không cần await
                    _posRequestHandler.SendUpdateDeliveryRequest(new UpdateDeliveryRequest
                    {
                        RequestToken = requestToken,
                        Items = updatingItems.MapToList<DeliveryInfoDto>(),
                    }).ConfigureAwait(false);
                }
            }
            catch (Exception ex)
            {
                //_logger.LogError(ex, $"UpdateDeliveryInfoWork: {JsonSerializer.Serialize(updatingItems)}");
            }

        }

        /// <summary>
        /// Xóa bản ghi DeliveryInfoDto khi co request từ bên Vipos
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async ValueTask<BaseResponse> DeleteByPosRequest(DeliveryInfoDto input)
        {
            var item = input.Id > 0 && !string.IsNullOrEmpty(input.RequestToken)
                     ? await _deliveryInfoRepository.GetAll().Where(x => x.Id == input.Id && input.RequestToken.Equals(x.RequestToken)).FirstOrDefaultAsync()
                     : null;
            if (item != null)
            {
                _deliveryInfoRepository.Delete(item);
                await _deliveryInfoRepository.SaveChangesAsync();
            }
            return new BaseResponse() { Success = true };
        }
    }
}
