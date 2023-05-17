using EventAPI.DomainModel;

namespace EventAPI.Infrastructure.Repository
{
    public interface IGenericDbRepository<TEntity> where TEntity : ModelBase
    {
        Task AddModel(TEntity entity);
        Task DeleteModel(TEntity entity);
        Task UpdateModel(TEntity entity);

        Task<IEnumerable<TEntity>> GetAll();
        Task<TEntity> GetByPrimaryKey(int id);
    }
}