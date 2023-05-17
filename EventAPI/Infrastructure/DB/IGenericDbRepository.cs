using EventAPI.DomainModel;

namespace EventAPI.Infrastructure.DB
{
    public interface IGenericDbRepository<TEntity> where TEntity : ModelBase
    {
        void AddModel(TEntity entity);
        void DeleteModel(TEntity entity);
        Task<TEntity> FindByPrimaryKey(int id);
        void UpdateModel(TEntity entity);
    }
}