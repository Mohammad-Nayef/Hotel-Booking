namespace HotelBooking.Domain.Abstractions.Repositories
{
    public interface IRepository<TEntity> where TEntity : Entity
    {
        Task<List<TEntity>> GetAllAsync();
        Task<List<TEntity>> GetAllAsync(int itemsToTake, int itemsToSkip);
        Task<TEntity> GetByIdAsync(Guid id);
        Task AddAsync(TEntity entity);
        Task UpdateAsync(TEntity entity);
        Task DeleteAsync(TEntity entity);
        Task<int> GetCountAsync();
        Task<bool> ExistsAsync(Guid id);
    }
}
