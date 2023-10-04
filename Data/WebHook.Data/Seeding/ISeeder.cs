namespace WebHook.EntityFrameWork.Seeding
{
    using System;
    using System.Threading.Tasks;
    using WebHook.EntityFrameWork;

    public interface ISeeder
    {
        Task SeedAsync(ApplicationDbContext dbContext, IServiceProvider serviceProvider);
    }
}
