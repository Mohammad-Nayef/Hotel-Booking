using HotelBooking.Application.Validations;
using HotelBooking.Domain.Abstractions;
using HotelBooking.Domain.Abstractions.Repositories;
using HotelBooking.Domain.Abstractions.Services;
using HotelBooking.Domain.Exceptions;

namespace HotelBooking.Application.Services
{
    internal class EntityService<TEntity> : IEntityService<TEntity> where TEntity : Entity
    {
        private readonly IRepository<TEntity> _repository;

        public EntityService(IRepository<TEntity> repository)
        {
            _repository = repository;
        }

        public async Task AddAsync(TEntity entity)
        {
            ArgumentNullException.ThrowIfNull(entity, nameof(entity));

            if (await _repository.ExistsAsync(entity.Id))
                throw new IdDuplicationException(entity.Id);

            await _repository.AddAsync(entity);
        }

        public async Task DeleteAsync(Guid id)
        {
            var entityToDelete = await _repository.GetByIdAsync(id);

            if (entityToDelete == null)
                throw new KeyNotFoundException($"Entity with Id '{id}' does not exist.");

            await _repository.DeleteAsync(entityToDelete);
        }

        public async Task<List<TEntity>> GetAllAsync() =>
            await _repository.GetAllAsync();

        public async Task<TEntity> GetByIdAsync(Guid id)
        {
            var entity = await _repository.GetByIdAsync(id);

            if (entity == null)
                throw new KeyNotFoundException($"Entity with Id '{id}' does not exist.");

            return entity;
        }

        public async Task<List<TEntity>> GetByPageAsync(int pageNumber, int pageSize)
        {
            PaginationValidator.Validate(pageNumber, pageSize);

            return await _repository.GetAllAsync((pageNumber - 1) * pageSize, pageSize);
        }

        public async Task<int> GetCount() => await _repository.GetCountAsync();

        public async Task UpdateAsync(TEntity entity)
        {
            ArgumentNullException.ThrowIfNull(entity, nameof(entity));

            if (!await _repository.ExistsAsync(entity.Id))
                throw new KeyNotFoundException($"Entity with Id '{entity.Id}' does not exist.");

            await _repository.UpdateAsync(entity);
        }
    }
}
