using AutoMapper;
using HotelBooking.Domain.Abstractions.Repositories;
using HotelBooking.Domain.Models;
using HotelBooking.Infrastructure.Tables;
using Microsoft.EntityFrameworkCore;

namespace HotelBooking.Infrastructure.Repositories
{
    /// <inheritdoc cref="IHotelReviewRepository"/>
    internal class HotelReviewRepository : IHotelReviewRepository
    {
        private readonly HotelsBookingDbContext _dbContext;
        private readonly IMapper _mapper;

        public HotelReviewRepository(HotelsBookingDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public async Task AddAsync(HotelReviewDTO newReview)
        {
            await _dbContext.HotelReviews.AddAsync(_mapper.Map<HotelReviewTable>(newReview));
            await _dbContext.SaveChangesAsync();
        }

        public Task<bool> ExistsByUserAndHotelAsync(Guid userId, Guid hotelId)
        {
            return _dbContext.HotelReviews.AnyAsync(review =>
                review.UserId == userId &&
                review.HotelId == hotelId);
        }

        public IEnumerable<ReviewForHotelPageDTO> GetReviewsByHotelByPage(
            Guid hotelId, int itemsToSkip, int itemsToTake)
        {
            var reviews = _dbContext.HotelReviews
                .Where(review => review.HotelId == hotelId)
                .Include(review => review.User)
                .Skip(itemsToSkip)
                .Take(itemsToTake);

            return _mapper.Map<IEnumerable<ReviewForHotelPageDTO>>(reviews);
        }

        public async Task<int> GetReviewsByHotelCountAsync(Guid hotelId)
        {
            var reviews = _dbContext.HotelReviews
                .Where(review => review.HotelId == hotelId);

            if (reviews.TryGetNonEnumeratedCount(out int count))
                return count;

            return await reviews.CountAsync();
        }
    }
}
