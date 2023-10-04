namespace WebHook.Services.Repositories
{
    using EFCore.BulkExtensions;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    public interface IRepository<TEntity> : IDisposable
        where TEntity : class
    {
        IQueryable<TEntity> GetAll();

        IQueryable<TEntity> AllAsNoTracking();

        Task AddAsync(TEntity entity);

        void Update(TEntity entity);

        void Delete(TEntity entity);

        Task<int> SaveChangesAsync();
        Task BulkDeleteAsync(List<TEntity> entities, Action<BulkConfig> bulkAction = null);
        Task BulkUpdateAsync(List<TEntity> entities, Action<BulkConfig> bulkAction = null);
        Task BulkInsertAsync(List<TEntity> entities, Action<BulkConfig> bulkAction = null);
    }
}
