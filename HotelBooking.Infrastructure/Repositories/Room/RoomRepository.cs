using AutoMapper;
using HotelBooking.Domain.Abstractions.Repositories.Room;
using HotelBooking.Domain.Models.Room;
using HotelBooking.Infrastructure.Tables;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace HotelBooking.Infrastructure.Repositories.Room
{
    /// <inheritdoc cref="IRoomRepository"/>
    internal class RoomRepository : IRoomRepository
    {
        private readonly HotelsBookingDbContext _dbContext;
        private readonly IMapper _mapper;
        private readonly ILogger<RoomRepository> _logger;

        public RoomRepository(
            HotelsBookingDbContext dbContext, IMapper mapper, ILogger<RoomRepository> logger)
        {
            _dbContext = dbContext;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<Guid> AddAsync(RoomDTO newRoom)
        {
            var entityEntry = await _dbContext.Rooms.AddAsync(_mapper.Map<RoomTable>(newRoom));
            await _dbContext.SaveChangesAsync();

            _logger.LogInformation("Created room with Id: {id}", entityEntry.Entity.Id);
            return entityEntry.Entity.Id;
        }

        public async Task DeleteAsync(Guid id)
        {
            _dbContext.Rooms.Remove(new RoomTable { Id = id });
            await _dbContext.SaveChangesAsync();
            _logger.LogInformation("Deleted room with Id: {id}", id);
        }

        public Task<bool> ExistsAsync(Guid id) =>
            _dbContext.Rooms.AnyAsync(room => room.Id == id);

        public async Task<RoomDTO> GetByIdAsync(Guid id)
        {
            return _mapper.Map<RoomDTO>(
                await _dbContext.Rooms
                    .FindAsync(id));
        }

        public async Task<int> GetCountAsync()
        {
            if (_dbContext.Rooms.TryGetNonEnumeratedCount(out var count))
                return count;

            return await _dbContext.Rooms.CountAsync();
        }

        public async Task UpdateAsync(RoomDTO room)
        {
            _dbContext.Rooms.Update(_mapper.Map<RoomTable>(room));
            await _dbContext.SaveChangesAsync();
        }
    }
}
