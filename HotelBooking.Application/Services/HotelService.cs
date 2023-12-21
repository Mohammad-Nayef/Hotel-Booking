using FluentValidation;
using HotelBooking.Domain.Abstractions.Repositories;
using HotelBooking.Domain.Abstractions.Services;
using HotelBooking.Domain.Models;

namespace HotelBooking.Application.Services
{
    internal class HotelService : IHotelService
    {
        private readonly IHotelRepository _hotelRepository;
        private readonly IValidator<PaginationDTO> _paginationValidator;
        private readonly IValidator<HotelDTO> _hotelValidator;

        public HotelService(
            IHotelRepository hotelRepository,
            IValidator<PaginationDTO> paginationValidator,
            IValidator<HotelDTO> hotelValidator)
        {
            _hotelRepository = hotelRepository;
            _paginationValidator = paginationValidator;
            _hotelValidator = hotelValidator;
        }

        public async Task<Guid> AddAsync(HotelDTO hotel)
        {
            await _hotelValidator.ValidateAndThrowAsync(hotel);

            hotel.CreationDate = DateTime.UtcNow;
            hotel.ModificationDate = DateTime.UtcNow;

            return await _hotelRepository.AddAsync(hotel);
        }

        public async Task DeleteAsync(Guid id)
        {
            if (!await _hotelRepository.ExistsAsync(id))
                throw new KeyNotFoundException($"The Id '{id}' does not exist.");

            await _hotelRepository.DeleteAsync(id);
        }

        public Task<bool> ExistsAsync(Guid id) => _hotelRepository.ExistsAsync(id);

        public Task<int> GetCountAsync() => _hotelRepository.GetCountAsync();

        public async Task<IEnumerable<HotelForAdminDTO>> GetForAdminByPageAsync(
            PaginationDTO pagination)
        {
            await _paginationValidator.ValidateAndThrowAsync(pagination);

            return _hotelRepository.GetForAdminByPage(
                (pagination.PageNumber - 1) * pagination.PageSize,
                pagination.PageSize);
        }
    }
}
