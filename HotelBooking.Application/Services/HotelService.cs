using System.Linq.Expressions;
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
            await ValidateIdAsync(id);

            await _hotelRepository.DeleteAsync(id);
        }

        public Task<bool> ExistsAsync(Guid id) => _hotelRepository.ExistsAsync(id);

        public async Task<HotelDTO> GetByIdAsync(Guid id)
        {
            await ValidateIdAsync(id);

            return await _hotelRepository.GetByIdAsync(id);
        }

        private async Task ValidateIdAsync(Guid id)
        {
            if (!await ExistsAsync(id))
                throw new KeyNotFoundException($"The Id '{id}' does not exist.");
        }

        public Task<int> GetCountAsync() => _hotelRepository.GetCountAsync();

        public async Task<IEnumerable<HotelForAdminDTO>> GetForAdminByPageAsync(
            PaginationDTO pagination)
        {
            await _paginationValidator.ValidateAndThrowAsync(pagination);

            return _hotelRepository.GetForAdminByPage(
                (pagination.PageNumber - 1) * pagination.PageSize,
                pagination.PageSize);
        }

        public async Task UpdateAsync(HotelDTO hotel)
        {
            await _hotelValidator.ValidateAndThrowAsync(hotel);

            hotel.ModificationDate = DateTime.UtcNow;

            await _hotelRepository.UpdateAsync(hotel);
        }

        public async Task<IEnumerable<HotelForAdminDTO>> SearchByHotelForAdminByPageAsync(
            PaginationDTO pagination, string searchQuery)
        {
            await _paginationValidator.ValidateAndThrowAsync(pagination);

            return _hotelRepository.SearchByHotelForAdminByPage(
                (pagination.PageNumber - 1) * pagination.PageSize,
                pagination.PageSize,
                ToSearchExpression(searchQuery));
        }

        private Expression<Func<HotelForAdminDTO, bool>> ToSearchExpression(string searchQuery)
        {
            searchQuery = searchQuery.ToLower();

            return hotel =>
                hotel.Name.ToLower().Contains(searchQuery) ||
                hotel.OwnerName.ToLower().Contains(searchQuery);
        }

        public Task<int> GetSearchByHotelForAdminCountAsync(string searchQuery) =>
            _hotelRepository.GetSearchByHotelForAdminCountAsync(ToSearchExpression(searchQuery));
    }
}
