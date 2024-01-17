﻿using AutoMapper;
using HotelBooking.Db.Tables;
using HotelBooking.Domain.Abstractions.Repositories.City;
using HotelBooking.Domain.Models.City;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.Extensions.Logging;

namespace HotelBooking.Db.Repositories.City
{
    internal class CityRepository : ICityRepository
    {
        private readonly HotelsBookingDbContext _dbContext;
        private readonly IMapper _mapper;
        private readonly ILogger<CityRepository> _logger;

        public CityRepository(
            HotelsBookingDbContext dbContext, IMapper mapper, ILogger<CityRepository> logger)
        {
            _dbContext = dbContext;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<Guid> AddAsync(CityDTO newCity)
        {
            var entityEntry = await _dbContext.Cities.AddAsync(_mapper.Map<CityTable>(newCity));
            await _dbContext.SaveChangesAsync();

            _logger.LogInformation("Created city with Id: {id}", entityEntry.Entity.Id);
            return entityEntry.Entity.Id;
        }

        public async Task DeleteAsync(Guid id)
        {
            _dbContext.Cities.Remove(new CityTable { Id = id });
            await _dbContext.SaveChangesAsync();
            _logger.LogInformation("Deleted city with Id: {id}", id);
        }

        public Task<bool> ExistsAsync(Guid id) =>
            _dbContext.Cities.AnyAsync(city => city.Id == id);

        public async Task<CityDTO> GetByIdAsync(Guid id)
        {
            return _mapper.Map<CityDTO>(
                await _dbContext.Cities
                .AsNoTracking()
                .FirstOrDefaultAsync());
        }

        public async Task<int> GetCountAsync()
        {
            if (_dbContext.Cities.TryGetNonEnumeratedCount(out var count))
                return count;

            return await _dbContext.Cities.CountAsync();
        }

        public async Task UpdateAsync(CityDTO city)
        {
            _dbContext.Cities.Update(_mapper.Map<CityTable>(city));
            await _dbContext.SaveChangesAsync();
        }
    }
}