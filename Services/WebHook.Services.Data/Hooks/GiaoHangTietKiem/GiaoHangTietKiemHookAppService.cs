using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Threading.Tasks;
using WebHook.Data.Common.Enum;
using WebHook.Data.Models.AppSettings;
using WebHook.Data.Models.DeliveryInfos.Entities;
using WebHook.Data.Models.GiaoHangTietKiem.Dtos;
using WebHook.Services.Hooks.Base;
using WebHook.Services.Repositories;

namespace WebHook.Services.Hooks.GiaoHangTietKiem
{
    public class GiaoHangTietKiemHookAppService : WebHookBaseAppService, IGiaoHangTietKiemHookAppService
    {
        private readonly IRepository<DeliveryInfo> _deliveryInfoRepository;
        private readonly GiaoHangTietKiemSettings _settings;
        public GiaoHangTietKiemHookAppService(
            IHttpContextAccessor httpContextAccessor,
            IRepository<DeliveryInfo> deliveryInfoRepository,
            IOptions<AppSettings> appSettings) : base(httpContextAccessor)
        {
            _deliveryInfoRepository = deliveryInfoRepository;
            _settings = appSettings.Value.WebHookSettings.GiaoHangTietKiem;
        }

        /// <summary>
        /// Hook 
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<BaseResponse> UpdateStatusDelivery(UpdateStatusDeliveryInput input)
        {
            if (input == null)
                return new BaseResponse() { Success = false };

            //Check domain, ip, token của ghtk
            AuthorizeRequest(_settings.IpWhiteList, _settings.AccessToken);

            var deliveryInfo = new DeliveryInfo(input.PartnerId)
            {
                POSCode = input.PartnerId,
                DeliveryCode = input.LabelId,
                DeliveryPartnerType = DeliveryPartnerType.GiaoHangTietKiem,
                DataJson = JsonSerializer.Serialize(input),
                CreatedOn = DateTime.Now
            };

            await _deliveryInfoRepository.BulkInsertAsync(new List<DeliveryInfo> { deliveryInfo }, opt => { opt.SetOutputIdentity = true; });

            return new BaseResponse { Success = true, Message = "Succeed" };
        }
    }
}
