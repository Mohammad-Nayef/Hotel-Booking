﻿using AutoMapper;
using HotelBooking.Db.Tables;
using HotelBooking.Domain.Abstractions.Repositories;
using HotelBooking.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace HotelBooking.Db.Repositories
{
    internal class RoomRepository : IRoomRepository
    {
        private readonly HotelsBookingDbContext _dbContext;
        private readonly IMapper _mapper;

        public RoomRepository(HotelsBookingDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public async Task<Guid> AddAsync(RoomDTO newRoom)
        {
            var entityEntry = await _dbContext.Rooms.AddAsync(_mapper.Map<RoomTable>(newRoom));
            await _dbContext.SaveChangesAsync();

            return entityEntry.Entity.Id;
        }

        public async Task DeleteAsync(Guid id)
        {
            _dbContext.Rooms.Remove(new RoomTable { Id = id });
            await _dbContext.SaveChangesAsync();
        }

        public Task<bool> ExistsAsync(Guid id) =>
            _dbContext.Rooms.AnyAsync(room => room.Id == id);

        public async Task<int> GetCountAsync()
        {
            if (_dbContext.Rooms.TryGetNonEnumeratedCount(out var count))
                return count;

            return await _dbContext.Rooms.CountAsync();
        }

        public IEnumerable<RoomForAdminDTO> GetForAdminByPage(
            int itemsToTake, int itemsToSkip)
        {
            return _dbContext.RoomsForAdmin
                .Take(itemsToTake)
                .Skip(itemsToSkip)
                .AsEnumerable();
        }
    }
}