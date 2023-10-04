namespace WebHook.Data.Models.Application
{
    using WebHook.Data.Common.Models;

    public class Setting : BaseDeletableEntity<int>
    {
        public string Name { get; set; }

        public string Value { get; set; }
    }
}
