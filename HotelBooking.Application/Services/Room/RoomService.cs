using FluentValidation;
using HotelBooking.Domain.Abstractions.Repositories.Room;
using HotelBooking.Domain.Abstractions.Services.Room;
using HotelBooking.Domain.Models.Room;

namespace HotelBooking.Application.Services.Room
{
    /// <inheritdoc cref="IRoomService"/>
    internal class RoomService : IRoomService
    {
        private readonly IRoomRepository _roomRepository;
        private readonly IValidator<RoomDTO> _roomValidator;

        public RoomService(
            IRoomRepository roomRepository, IValidator<RoomDTO> roomValidator)
        {
            _roomRepository = roomRepository;
            _roomValidator = roomValidator;
        }

        public async Task<Guid> AddAsync(RoomDTO newRoom)
        {
            await _roomValidator.ValidateAndThrowAsync(newRoom);

            newRoom.CreationDate = DateTime.UtcNow;
            newRoom.ModificationDate = DateTime.UtcNow;

            return await _roomRepository.AddAsync(newRoom);
        }

        public async Task DeleteAsync(Guid id)
        {
            await ValidateIdAsync(id);

            await _roomRepository.DeleteAsync(id);
        }

        public async Task ValidateIdAsync(Guid id)
        {
            if (!await ExistsAsync(id))
                throw new KeyNotFoundException($"The Id '{id}' does not exist.");
        }

        public Task<bool> ExistsAsync(Guid id) => _roomRepository.ExistsAsync(id);

        public async Task<RoomDTO> GetByIdAsync(Guid id)
        {
            await ValidateIdAsync(id);

            return await _roomRepository.GetByIdAsync(id);
        }

        public Task<int> GetCountAsync() => _roomRepository.GetCountAsync();

        public async Task UpdateAsync(RoomDTO room)
        {
            await _roomValidator.ValidateAndThrowAsync(room);

            room.ModificationDate = DateTime.UtcNow;

            await _roomRepository.UpdateAsync(room);
        }
    }
}
