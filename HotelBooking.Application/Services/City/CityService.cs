﻿using FluentValidation;
using HotelBooking.Domain.Abstractions.Repositories.City;
using HotelBooking.Domain.Abstractions.Services.City;
using HotelBooking.Domain.Models.City;

namespace HotelBooking.Application.Services.City
{
    internal class CityService : ICityService
    {
        private readonly ICityRepository _cityRepository;
        private readonly IValidator<CityDTO> _cityValidator;

        public CityService(
            ICityRepository cityRepository, IValidator<CityDTO> cityValidator)
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
            await ValidateIdAsync(id);

            await _cityRepository.DeleteAsync(id);
        }

        public Task<bool> ExistsAsync(Guid id) => _cityRepository.ExistsAsync(id);

        public async Task<CityDTO> GetByIdAsync(Guid id)
        {
            await ValidateIdAsync(id);

            return await _cityRepository.GetByIdAsync(id);
        }

        public Task<int> GetCountAsync() => _cityRepository.GetCountAsync();

        public async Task UpdateAsync(CityDTO city)
        {
            await _cityValidator.ValidateAndThrowAsync(city);

            city.ModificationDate = DateTime.UtcNow;

            await _cityRepository.UpdateAsync(city);
        }

        public async Task ValidateIdAsync(Guid id)
        {
            if (!await ExistsAsync(id))
                throw new KeyNotFoundException($"The Id '{id}' does not exist.");
        }
    }
}
