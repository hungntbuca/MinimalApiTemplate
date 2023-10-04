using System.Collections.Generic;

namespace WebHook.Data.Models.AppSettings
{
    public class BaseWebHookSettings
    {
        public List<string> IpWhiteList { get; set; }
        public string AccessToken { get; set; }
    }
}
