using System.Linq.Expressions;
using AutoMapper;
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

        public async Task<HotelDTO> GetByIdAsync(Guid id) => 
            _mapper.Map<HotelDTO>(await _dbContext.Hotels
                .AsNoTracking()
                .FirstOrDefaultAsync());

        public async Task<int> GetCountAsync()
        {
            if (_dbContext.Hotels.TryGetNonEnumeratedCount(out var count))
                return count;

            return await _dbContext.Hotels.CountAsync();
        }

        public IEnumerable<HotelForAdminDTO> GetForAdminByPage(int itemsToSkip, int itemsToTake) =>
            _dbContext.HotelsForAdmin
                .OrderBy(hotel => hotel.Name)
                .Skip(itemsToSkip)
                .Take(itemsToTake)
                .AsNoTracking()
                .AsEnumerable();

        public IEnumerable<HotelForAdminDTO> SearchByHotelForAdminByPage(
            int itemsToSkip,
            int itemsToTake,
            Expression<Func<HotelForAdminDTO, bool>> searchExpression)
        {
            return _dbContext.HotelsForAdmin
                .Where(searchExpression)
                .OrderBy(hotel => hotel.Name)
                .Skip(itemsToSkip)
                .Take(itemsToTake)
                .AsNoTracking()
                .AsEnumerable();
        }

        public Task<int> GetSearchByHotelForAdminCountAsync(
            Expression<Func<HotelForAdminDTO, bool>> searchExpression)
        {
            return _dbContext.HotelsForAdmin
                .Where(searchExpression)
                .CountAsync();
        }

        public async Task UpdateAsync(HotelDTO hotel)
        {
            _dbContext.Hotels.Update(_mapper.Map<HotelTable>(hotel));
            await _dbContext.SaveChangesAsync();
        }

        public Task<int> GetNumberOfImagesAsync(Guid hotelId)
        {
            throw new NotImplementedException();
        }
    }
}
