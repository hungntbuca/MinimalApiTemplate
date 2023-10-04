using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Primitives;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using WebHook.Data.Models.Common;

namespace WebHook.Services.Hooks.Base
{
    public class WebHookBaseAppService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public WebHookBaseAppService(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        protected void AuthorizeRequest(List<string> allowedIps, string token)
        {
            AuthorizeIpAddress(allowedIps);
            AuthorizeToken(token);
        }

        protected void AuthorizeIpAddress(List<string> allowedIps)
        {
            var ipAddress = _httpContextAccessor.HttpContext?.Connection.RemoteIpAddress;

            if (ipAddress == null || !IsIpAddressAllowed(ipAddress, allowedIps))
            {
                throw new UnauthorizedAccessException();
            }
        }
        protected bool IsIpAddressAllowed(IPAddress ipAddress, List<string> allowedIps)
        {
            return allowedIps.Any(ip => IPAddressRange.Parse(ip).Contains(ipAddress));
        }

        protected void AuthorizeToken(string token)
        {
            StringValues headerToken = new StringValues();

            _httpContextAccessor.HttpContext?.Request.Headers.TryGetValue("Token", out headerToken);

            var requestToken = headerToken.FirstOrDefault();

            if (string.IsNullOrEmpty(requestToken) || !requestToken.Equals(token))
                throw new UnauthorizedAccessException();
        }
    }
}
