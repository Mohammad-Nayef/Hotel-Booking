using FluentValidation;
using HotelBooking.Application.Validators;
using HotelBooking.Domain.Abstractions.Repositories;
using HotelBooking.Domain.Abstractions.Services;
using HotelBooking.Domain.Entities;
using HotelBooking.Domain.Models;

namespace HotelBooking.Application.Services
{
    internal class CityService : ICityService
    {
        private readonly ICityRepository _cityRepository;
        private readonly IValidator<CityDTO> _cityValidator;
        private readonly IValidator<PaginationDTO> _paginationValidator;

        public CityService(
            ICityRepository cityRepository,
            IValidator<CityDTO> cityValidator,
            IValidator<PaginationDTO> paginationValidator)
        {
            _cityRepository = cityRepository;
            _cityValidator = cityValidator;
            _paginationValidator = paginationValidator;
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
            await ValidateIdAsync(id);

            await _cityRepository.DeleteAsync(id);
        }

        private async Task ValidateIdAsync(Guid id)
        {
            if (!await ExistsAsync(id))
                throw new KeyNotFoundException($"The Id '{id}' does not exist.");
        }

        public Task<bool> ExistsAsync(Guid id) => _cityRepository.ExistsAsync(id);

        public async Task<CityDTO> GetByIdAsync(Guid id)
        {
            await ValidateIdAsync(id);

            return await _cityRepository.GetByIdAsync(id);
        }

        public Task<int> GetCountAsync() => _cityRepository.GetCountAsync();

        public async Task<IEnumerable<CityForAdminDTO>> GetForAdminByPageAsync(
            PaginationDTO pagination)
        {
            await _paginationValidator.ValidateAndThrowAsync(pagination);

            return _cityRepository.GetForAdminByPage(
                (pagination.PageNumber - 1) * pagination.PageSize,
                pagination.PageSize);
        }

        public async Task UpdateAsync(CityDTO city)
        {
            await _cityValidator.ValidateAndThrowAsync(city);

            city.ModificationDate = DateTime.UtcNow;

            await _cityRepository.UpdateAsync(city);
        }
    }
}
