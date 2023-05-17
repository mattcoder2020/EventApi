using EventAPI.DomainModel;
using Microsoft.EntityFrameworkCore;

namespace EventAPI.Infrastructure.DB
{

    public class GenericDbRepository<TEntity> : IGenericDbRepository<TEntity> where TEntity : ModelBase
    {
        DbContext _dbcontext;
        public GenericDbRepository(EventDbContext dbcontext)
        {
            _dbcontext = dbcontext;
        }
        public void AddModel(TEntity entity)
        {
            _dbcontext.Set<TEntity>().Add(entity);
        }

        public void DeleteModel(TEntity entity)
        {
            _dbcontext.Set<TEntity>().Remove(entity);
        }

        public void UpdateModel(TEntity entity)
        {
            _dbcontext.Set<TEntity>().Update(entity);
        }

        public async Task<TEntity> FindByPrimaryKey(int id)
        {
            return await _dbcontext.Set<TEntity>().FindAsync(id);
        }
    }
}
