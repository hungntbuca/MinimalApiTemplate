namespace WebHook.Data.Models.AppSettings
{

    public class UpdateDeliveryBackgroundJobSettings
    {
        public int UpdateScheduleWithIntervalInMinutes { get; set; }
        public int ResendScheduleWithIntervalInMinutes { get; set; }
        public int MaxItemsPerRequest { get; set; }
    }
}
