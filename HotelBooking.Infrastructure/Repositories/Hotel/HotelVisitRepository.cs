using AutoMapper;
using HotelBooking.Domain.Abstractions.Repositories.Hotel;
using HotelBooking.Domain.Models;
using HotelBooking.Infrastructure.Tables;

namespace HotelBooking.Infrastructure.Repositories.Hotel
{
    /// <inheritdoc cref="IHotelVisitRepository"/>
    internal class HotelVisitRepository : IHotelVisitRepository
    {
        private readonly HotelsBookingDbContext _dbContext;
        private readonly IMapper _mapper;

        public HotelVisitRepository(HotelsBookingDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public async Task AddAsync(HotelVisitDTO newVisit)
        {
            await _dbContext.HotelVisits.AddAsync(
                _mapper.Map<HotelVisitTable>(newVisit));
            await _dbContext.SaveChangesAsync();
        }
    }
}
