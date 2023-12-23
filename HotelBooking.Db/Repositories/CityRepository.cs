using System.Linq.Expressions;
using AutoMapper;
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

        public async Task<CityDTO> GetByIdAsync(Guid id) =>
            _mapper.Map<CityDTO>(await _dbContext.Cities.AsNoTracking().FirstOrDefaultAsync());

        public async Task<int> GetCountAsync()
        {
            if (_dbContext.Cities.TryGetNonEnumeratedCount(out var count))
                return count;

            return await _dbContext.Cities.CountAsync();
        }

        public IEnumerable<CityForAdminDTO> GetForAdminByPage(int itemsToSkip, int itemsToTake)
        {
            return _dbContext.CitiesForAdmin
                .OrderBy(city => city.Name)
                .Skip(itemsToSkip)
                .Take(itemsToTake)
                .AsNoTracking()
                .AsEnumerable();
        }

        public IEnumerable<CityForAdminDTO> SearchByCityForAdminByPage(
            int itemsToSkip,
            int itemsToTake,
            Expression<Func<CityForAdminDTO, bool>> searchExpression)
        {
            return _dbContext.CitiesForAdmin
                .Where(searchExpression)
                .OrderBy(city => city.Name)
                .Skip(itemsToSkip)
                .Take(itemsToTake)
                .AsNoTracking()
                .AsEnumerable();
        }

        public Task<int> GetSearchByCityForAdminCountAsync(
            Expression<Func<CityForAdminDTO, bool>> searchExpression)
        {
            return _dbContext.CitiesForAdmin
                .Where(searchExpression)
                .CountAsync();
        }

        public async Task UpdateAsync(CityDTO city)
        {
            _dbContext.Cities.Update(_mapper.Map<CityTable>(city));
            await _dbContext.SaveChangesAsync();
        }
    }
}
