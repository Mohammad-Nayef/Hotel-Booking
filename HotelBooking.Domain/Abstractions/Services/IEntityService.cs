namespace HotelBooking.Domain.Abstractions.Services
{
    public interface IEntityService<TEntity> where TEntity : Entity
    {
        Task<List<TEntity>> GetAllAsync();

        /// <summary>
        /// Retrieves entities by pages.
        /// If pagination info are invalid, and exception is thrown containing the invalid info.
        /// </summary>
        /// <param name="pageNumber"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        Task<List<TEntity>> GetByPageAsync(int pageNumber, int pageSize);
        /// <exception cref="KeyNotFoundException"></exception>
        Task<TEntity> GetByIdAsync(Guid id);
        Task AddAsync(TEntity entity);
        Task UpdateAsync(TEntity entity);
        /// <exception cref="KeyNotFoundException"></exception>
        Task DeleteAsync(Guid id);
        Task<int> GetCount();
    }
}
