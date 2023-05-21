using EventAPI.DomainModel;
using System.Linq.Expressions;

namespace EventAPI.Infrastructure.Repository
{
    public interface IGenericDbRepository<TEntity> where TEntity : ModelBase
    {
        Task AddModelAsync(TEntity entity);
        Task DeleteModelAsync(TEntity entity);
        Task UpdateModelAsync(TEntity entity);

        Task<IEnumerable<TEntity>> GetAllAsync();
        Task<TEntity> GetByPrimaryKeyAsync(int id);
        Task<TEntity> GetByPrimaryKeyAsync(int id, Expression<Func<TEntity, object>>[] includeItem);
    }
}