﻿using AutoMapper;
using HotelBooking.Db.Tables;
using HotelBooking.Domain.Abstractions.Repositories;
using HotelBooking.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace HotelBooking.Db.Repositories
{
    internal class CityRepository : ICityRepository
    {
        private readonly HotelsBookingDbContext _dbContext;
        private readonly IMapper _mapper;

        public CityRepository(HotelsBookingDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public async Task<Guid> AddAsync(CityDTO newCity)
        {
            var entityEntry = await _dbContext.Cities.AddAsync(_mapper.Map<CityTable>(newCity));
            await _dbContext.SaveChangesAsync();

            return entityEntry.Entity.Id;
        }

        public async Task DeleteAsync(Guid id)
        {
            _dbContext.Cities.Remove(new CityTable { Id = id });
            await _dbContext.SaveChangesAsync();
        }

        public Task<bool> ExistsAsync(Guid id) =>
            _dbContext.Cities.AnyAsync(city => city.Id == id);

        public async Task<int> GetCountAsync()
        {
            if (_dbContext.Cities.TryGetNonEnumeratedCount(out var count))
                return count;

            return await _dbContext.Cities.CountAsync();
        }

        public IEnumerable<CityForAdminDTO> GetForAdminByPage(int itemsToSkip, int itemsToTake)
        {
            return _dbContext.CitiesForAdmin
                .Skip(itemsToSkip)
                .Take(itemsToTake)
                .OrderBy(city => city.Name)
                .AsEnumerable();
        }
    }
}
