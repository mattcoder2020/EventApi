using EventAPI.DomainModel;
using EventAPI.Infrastructure.DataAccess;
using Microsoft.EntityFrameworkCore;

namespace EventAPI.Infrastructure.Repository
{

    public class GenericDbRepository<TEntity> : IGenericDbRepository<TEntity> where TEntity : ModelBase
    {
        DbContext _dbcontext;
        public GenericDbRepository(EventDbContext dbcontext)
        {
            _dbcontext = dbcontext;
        }
        public async Task AddModelAsync(TEntity entity)
        {
            _dbcontext.Set<TEntity>().Add(entity);
            await _dbcontext.SaveChangesAsync();
        }

        public async Task DeleteModelAsync(TEntity entity)
        {
            _dbcontext.Set<TEntity>().Remove(entity);
            await _dbcontext.SaveChangesAsync();
        }

        public async Task UpdateModelAsync(TEntity entity)
        {
            _dbcontext.Set<TEntity>().Update(entity);
            await _dbcontext.SaveChangesAsync();
        }

        public async Task<TEntity> GetByPrimaryKeyAsync(int id)
        {
            return await _dbcontext.Set<TEntity>().FindAsync(id);
        }
     

        public async Task<IEnumerable<TEntity>> GetAllAsync()
        {
            return await _dbcontext.Set<TEntity>().AsQueryable().ToArrayAsync<TEntity>();
        }

      }
}
