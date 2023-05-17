using EventAPI.DomainModel;
using EventAPI.Infrastructure.DB;
using Microsoft.AspNetCore.Mvc.RazorPages;
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
        public async void AddModel(TEntity entity)
        {
            _dbcontext.Set<TEntity>().Add(entity);
            await _dbcontext.SaveChangesAsync();
        }

        public async void DeleteModel(TEntity entity)
        {
            _dbcontext.Set<TEntity>().Remove(entity);
            await _dbcontext.SaveChangesAsync();
        }

        public async void UpdateModel(TEntity entity)
        {
            _dbcontext.Set<TEntity>().Update(entity);
            await _dbcontext.SaveChangesAsync();
        }

        public async Task<TEntity> GetByPrimaryKey(int id)
        {
            return await _dbcontext.Set<TEntity>().FindAsync(id);
        }
     

        public async Task<IEnumerable<TEntity>> GetAll()
        {
            return await _dbcontext.Set<TEntity>().AsQueryable().ToArrayAsync<TEntity>();
        }

      }
}
