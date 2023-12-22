using FluentValidation;
using HotelBooking.Domain.Abstractions.Repositories;
using HotelBooking.Domain.Abstractions.Services;
using HotelBooking.Domain.Models;

namespace HotelBooking.Application.Services
{
    internal class RoomService : IRoomService
    {
        private readonly IRoomRepository _roomRepository;
        private readonly IValidator<PaginationDTO> _paginationValidator;
        private readonly IValidator<RoomDTO> _roomValidator;

        public RoomService(
            IRoomRepository roomRepository,
            IValidator<PaginationDTO> paginationValidator,
            IValidator<RoomDTO> roomValidator)
        {
            _roomRepository = roomRepository;
            _paginationValidator = paginationValidator;
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
            if (!await ExistsAsync(id))
                throw new KeyNotFoundException($"The Id '{id}' does not exist.");

            await _roomRepository.DeleteAsync(id);
        }

        public Task<bool> ExistsAsync(Guid id) => _roomRepository.ExistsAsync(id);

        public Task<int> GetCountAsync() => _roomRepository.GetCountAsync();

        public async Task<IEnumerable<RoomForAdminDTO>> GetForAdminByPageAsync(
            PaginationDTO pagination)
        {
            await _paginationValidator.ValidateAndThrowAsync(pagination);

            return _roomRepository.GetForAdminByPage(
                (pagination.PageNumber - 1) * pagination.PageSize,
                pagination.PageSize);
        }
    }
}
