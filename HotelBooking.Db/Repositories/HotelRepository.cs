﻿using AutoMapper;
using HotelBooking.Db.Tables;
using HotelBooking.Domain.Abstractions.Repositories;
using HotelBooking.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace HotelBooking.Db.Repositories
{
    internal class HotelRepository : IHotelRepository
    {
        private readonly HotelsBookingDbContext _dbContext;
        private readonly IMapper _mapper;

        public HotelRepository(HotelsBookingDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public async Task<Guid> AddAsync(HotelDTO newHotel)
        {
            var entityEntry = await _dbContext.Hotels.AddAsync(_mapper.Map<HotelTable>(newHotel));
            await _dbContext.SaveChangesAsync();

            return entityEntry.Entity.Id;
        }

        public async Task DeleteAsync(Guid id)
        {
            _dbContext.Hotels.Remove(new HotelTable { Id = id });
            await _dbContext.SaveChangesAsync();
        }

        public Task<bool> ExistsAsync(Guid id) =>
            _dbContext.Hotels.AnyAsync(hotel => hotel.Id == id);

        public async Task<int> GetCountAsync()
        {
            if (_dbContext.Hotels.TryGetNonEnumeratedCount(out var count))
                return count;

            return await _dbContext.Hotels.CountAsync();
        }

        public IEnumerable<HotelForAdminDTO> GetForAdminByPage(
            int itemsToTake, int itemsToSkip)
        {
            return _dbContext.HotelsForAdmin
                .Take(itemsToTake)
                .Skip(itemsToSkip)
                .OrderBy(hotel => hotel.Name)
                .AsEnumerable();
        }
    }
}
