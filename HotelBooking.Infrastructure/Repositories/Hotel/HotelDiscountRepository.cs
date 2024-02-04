using AutoMapper;
using HotelBooking.Domain.Abstractions.Repositories.Hotel;
using HotelBooking.Domain.Models;
using HotelBooking.Domain.Models.Hotel;
using HotelBooking.Infrastructure;
using HotelBooking.Infrastructure.Extensions;
using HotelBooking.Infrastructure.Tables;
using Microsoft.EntityFrameworkCore;

namespace HotelBooking.Db.Repositories.Hotel
{
    /// <inheritdoc cref="IHotelDiscountRepository"/>
    internal class HotelDiscountRepository : IHotelDiscountRepository
    {
        private readonly HotelsBookingDbContext _dbContext;
        private readonly IMapper _mapper;

        public HotelDiscountRepository(HotelsBookingDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public IEnumerable<FeaturedHotelDTO> GetHotelsWithActiveDiscountsByPage(
            int itemsToSkip, int itemsToTake)
        {
            var hotels = GetHotelsWithActiveDiscounts()
                .Skip(itemsToSkip)
                .Take(itemsToTake);

            return _mapper.Map<IEnumerable<FeaturedHotelDTO>>(hotels);
        }

        public int GetHotelsWithActiveDiscountsCount()
        {
            var hotels = GetHotelsWithActiveDiscounts();

            if (hotels.TryGetNonEnumeratedCount(out var count))
                return count;

            return hotels.Count();
        }

        private IEnumerable<HotelTable> GetHotelsWithActiveDiscounts()
        {
            return _dbContext.Hotels
                .Include(hotel => hotel.City)
                .Include(hotel => hotel.Discounts)
                .AsEnumerable()
                .Where(hotel => hotel.Discounts.HasActiveDiscount());
        }

        public DiscountDTO GetHighestActiveDiscount(Guid hotelId)
        {
            var discountTable = _dbContext.Hotels
                .Include(hotel => hotel.Discounts)
                .SingleOrDefault(hotel => hotel.Id == hotelId)?
                .Discounts
                .GetHighestActive();

            return discountTable == null ? null : _mapper.Map<DiscountDTO>(discountTable);
        }
    }
}
