using HotelBooking.Domain.Abstractions;
using HotelBooking.Domain.Abstractions.Repositories;
using Microsoft.EntityFrameworkCore;

namespace HotelBooking.Db.Repositories
{
    internal class Repository<TEntity> : IRepository<TEntity> where TEntity : Entity
    {
        private readonly HotelsBookingDbContext _dbContext;

        public Repository(HotelsBookingDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task AddAsync(TEntity entity)
        {
            await _dbContext.AddAsync(entity);
            await _dbContext.SaveChangesAsync();
        }

        public async Task DeleteAsync(TEntity entity)
        {
            _dbContext.Remove(entity);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<List<TEntity>> GetAllAsync() =>
            await _dbContext.Set<TEntity>().ToListAsync();

        public async Task<List<TEntity>> GetAllAsync(int itemsToTake, int itemsToSkip) =>
            await _dbContext.Set<TEntity>()
                .Skip(itemsToTake)
                .Take(itemsToSkip)
                .ToListAsync();

        public async Task<TEntity> GetByIdAsync(Guid id) =>
            await _dbContext.FindAsync<TEntity>(id);

        public async Task UpdateAsync(TEntity entity)
        {
            _dbContext.Update(entity);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<int> GetCountAsync()
        {
            if (_dbContext.Set<TEntity>().TryGetNonEnumeratedCount(out var count))
                return count;

            return await _dbContext.Set<TEntity>().CountAsync();
        }

        public async Task<bool> ExistsAsync(Guid id) => 
            await _dbContext.Set<TEntity>().AnyAsync(x => x.Id == id);
    }
}
