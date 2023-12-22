using FluentValidation;
using HotelBooking.Domain.Abstractions.Repositories;
using HotelBooking.Domain.Abstractions.Services;
using HotelBooking.Domain.Models;

namespace HotelBooking.Application.Services
{
    internal class CityService : ICityService
    {
        private readonly ICityRepository _cityRepository;
        private readonly IValidator<CityDTO> _cityValidator;

        public CityService(ICityRepository cityRepository, IValidator<CityDTO> cityValidator)
        {
            _cityRepository = cityRepository;
            _cityValidator = cityValidator;
        }

        public async Task<Guid> AddAsync(CityDTO city)
        {
            await _cityValidator.ValidateAndThrowAsync(city);

            city.CreationDate = DateTime.UtcNow;
            city.ModificationDate = DateTime.UtcNow;

            return await _cityRepository.AddAsync(city);
        }

        public async Task DeleteAsync(Guid id)
        {
            if (!await _cityRepository.ExistsAsync(id))
                throw new KeyNotFoundException($"The Id '{id}' does not exist.");

            await _cityRepository.DeleteAsync(id);
        }

        public Task<bool> ExistsAsync(Guid id) => _cityRepository.ExistsAsync(id);

        public Task<int> GetCountAsync() => _cityRepository.GetCountAsync();

        public Task<IEnumerable<CityForAdminDTO>> GetForAdminByPageAsync(
            PaginationDTO pagination)
        {
            throw new NotImplementedException();
        }
    }
}
