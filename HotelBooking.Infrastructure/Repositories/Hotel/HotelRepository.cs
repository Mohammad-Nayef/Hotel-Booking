using AutoMapper;
using HotelBooking.Domain.Abstractions.Repositories.Hotel;
using HotelBooking.Domain.Models.Hotel;
using HotelBooking.Infrastructure.Tables;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace HotelBooking.Infrastructure.Repositories.Hotel
{
    /// <inheritdoc cref="IHotelRepository"/>
    internal class HotelRepository : IHotelRepository
    {
        private readonly HotelsBookingDbContext _dbContext;
        private readonly IMapper _mapper;
        private readonly ILogger<HotelRepository> _logger;

        public HotelRepository(
            HotelsBookingDbContext dbContext, IMapper mapper, ILogger<HotelRepository> logger)
        {
            _dbContext = dbContext;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<Guid> AddAsync(HotelDTO newHotel)
        {
            var entityEntry = await _dbContext.Hotels.AddAsync(
                _mapper.Map<HotelTable>(newHotel));
            await _dbContext.SaveChangesAsync();

            _logger.LogInformation("Created hotel with Id: {id}", entityEntry.Entity.Id);
            return entityEntry.Entity.Id;
        }

        public async Task DeleteAsync(Guid id)
        {
            _dbContext.Hotels.Remove(new HotelTable { Id = id });
            await _dbContext.SaveChangesAsync();
            _logger.LogInformation("Deleted hotel with Id: {id}", id);
        }

        public Task<bool> ExistsAsync(Guid id) =>
            _dbContext.Hotels.AnyAsync(hotel => hotel.Id == id);

        public async Task<HotelDTO> GetByIdAsync(Guid id)
        {
            return _mapper.Map<HotelDTO>(
                await _dbContext.Hotels
                    .FindAsync(id));
        }

        public async Task<int> GetCountAsync()
        {
            if (_dbContext.Hotels.TryGetNonEnumeratedCount(out var count))
                return count;

            return await _dbContext.Hotels.CountAsync();
        }

        public async Task UpdateAsync(HotelDTO hotel)
        {
            _dbContext.Hotels.Update(_mapper.Map<HotelTable>(hotel));
            await _dbContext.SaveChangesAsync();
        }
    }
}
