using AutoMapper;
using HotelBooking.Db.Tables;
using HotelBooking.Domain.Abstractions.Repositories.Hotel;
using HotelBooking.Domain.Models;

namespace HotelBooking.Db.Repositories.Hotel
{
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
