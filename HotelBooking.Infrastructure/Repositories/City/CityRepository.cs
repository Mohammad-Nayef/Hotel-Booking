using System.Data;
using AutoMapper;
using HotelBooking.Domain.Abstractions.Repositories.City;
using HotelBooking.Domain.Constants;
using HotelBooking.Domain.Models.City;
using HotelBooking.Infrastructure.Tables;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace HotelBooking.Infrastructure.Repositories.City
{
    /// <inheritdoc cref="ICityRepository"/>
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
                    .FindAsync(id));
        }

        public async Task<int> GetCountAsync()
        {
            if (_dbContext.Cities.TryGetNonEnumeratedCount(out var count))
                return count;

            return await _dbContext.Cities.CountAsync();
        }

        public IEnumerable<PopularCityDTO> GetPopularCitiesByPage(
            int itemsToSkip, int itemsToTake)
        {
            var cities = _dbContext.Cities
                .Include(city => city.Hotels)
                .ThenInclude(hotel => hotel.Visits
                    .Where(visit => visit.Date > HotelVisitConstants.LeastRecentVisitDate))
                .AsEnumerable()
                .OrderByDescending(city =>
                    city.Hotels.Sum(hotel => hotel.Visits.Count))
                .Skip(itemsToSkip)
                .Take(itemsToTake).ToList();

            return _mapper.Map<IEnumerable<PopularCityDTO>>(cities);
        }

        public async Task UpdateAsync(CityDTO city)
        {
            var cityTable = _mapper.Map<CityTable>(city);
            _dbContext.Cities.Update(cityTable);
            await _dbContext.SaveChangesAsync();
        }
    }
}
