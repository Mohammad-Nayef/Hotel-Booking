using AutoMapper;
using HotelBooking.Db.Tables;
using HotelBooking.Domain.Abstractions.Repositories.Hotel;
using HotelBooking.Domain.Models.Hotel;
using Microsoft.EntityFrameworkCore;

namespace HotelBooking.Db.Repositories.Hotel
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
            var entityEntry = await _dbContext.Hotels.AddAsync(
                _mapper.Map<HotelTable>(newHotel));
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

        public async Task<HotelDTO> GetByIdAsync(Guid id)
        {
            return _mapper.Map<HotelDTO>(
                await _dbContext.Hotels
                    .AsNoTracking()
                    .FirstOrDefaultAsync());
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
