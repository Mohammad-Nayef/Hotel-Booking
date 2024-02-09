using FluentValidation;
using HotelBooking.Domain.Abstractions.Repositories.Hotel;
using HotelBooking.Domain.Abstractions.Services.Hotel;
using HotelBooking.Domain.Models.Hotel;

namespace HotelBooking.Application.Services.Hotel
{
    /// <inheritdoc cref="IHotelService"/>
    internal class HotelService : IHotelService
    {
        private readonly IHotelRepository _hotelRepository;
        private readonly IValidator<HotelDTO> _hotelValidator;

        public HotelService(
            IHotelRepository hotelRepository, IValidator<HotelDTO> hotelValidator)
        {
            _hotelRepository = hotelRepository;
            _hotelValidator = hotelValidator;
        }

        public async Task<Guid> AddAsync(HotelDTO hotel)
        {
            await _hotelValidator.ValidateAndThrowAsync(hotel);

            hotel.CreationDate = DateTime.UtcNow;
            hotel.ModificationDate = DateTime.UtcNow;
            hotel.Geolocation = string.Concat(
                hotel.Geolocation.Where(character => !char.IsWhiteSpace(character)));

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

        public async Task ValidateIdAsync(Guid id)
        {
            if (!await ExistsAsync(id))
                throw new KeyNotFoundException($"The Id '{id}' does not exist.");
        }

        public Task<int> GetCountAsync() => _hotelRepository.GetCountAsync();

        public async Task UpdateAsync(HotelDTO hotel)
        {
            await _hotelValidator.ValidateAndThrowAsync(hotel);

            hotel.ModificationDate = DateTime.UtcNow;
            hotel.Geolocation = string.Concat(
                hotel.Geolocation.Where(character => !char.IsWhiteSpace(character)));
            await _hotelRepository.UpdateAsync(hotel);
        }
    }
}
