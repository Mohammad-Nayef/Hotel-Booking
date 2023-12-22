using AutoMapper;
using HotelBooking.Db.Tables;
using HotelBooking.Domain.Abstractions.Repositories;
using HotelBooking.Domain.Models;

namespace HotelBooking.Db.Repositories
{
    internal class DiscountRepository : IDiscountRepository
    {
        private readonly HotelsBookingDbContext _dbContext;
        private readonly IMapper _mapper;

        public DiscountRepository(HotelsBookingDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public async Task<Guid> AddAsync(DiscountDTO newDiscount)
        {
            var entityEntry = await _dbContext.Discounts.AddAsync(
                _mapper.Map<DiscountTable>(newDiscount));
            await _dbContext.SaveChangesAsync();

            return entityEntry.Entity.Id;
        }
    }
}
