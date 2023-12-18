namespace HotelBooking.Domain.Abstractions
{
    public interface IEntityService<TEntity> where TEntity : Entity
    {
        Task<IEnumerable<TEntity>> GetAllAsync();
        Task<IEnumerable<TEntity>> GetByPageAsync(int pageNumber, int pageSize);
        Task<TEntity> GetByIdAsync(Guid id);
        Task<bool> AddAsync(TEntity entity);
        Task<bool> UpdateAsync(TEntity entity);
        Task<bool> DeleteAsync(Guid id);
        Task<bool> ExistsAsync(int id);
    }
}
