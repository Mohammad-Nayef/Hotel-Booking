using HotelBooking.Domain.Abstractions.Repositories.Hotel;
using HotelBooking.Infrastructure.Tables;
using Microsoft.EntityFrameworkCore;

namespace HotelBooking.Infrastructure.Repositories.Hotel
{
    /// <inheritdoc cref="IHotelImageRepository"/>
    internal class HotelImageRepository : ImageRepository, IHotelImageRepository
    {
        private readonly HotelsBookingDbContext _dbContext;

        public HotelImageRepository(HotelsBookingDbContext dbContext) : base(dbContext, "Hotels")
        {
            _dbContext = dbContext;
        }

        protected override ImageTable CreateImageTable(
            Guid entityId, Guid imageId, string imagePath, string thumbnailPath)
        {
            return new ImageTable
            {
                Id = imageId,
                HotelId = entityId,
                Path = imagePath,
                ThumbnailPath = thumbnailPath
            };
        }

        public override Task<int> GetCountAsync(Guid hotelId)
        {
            return _dbContext.Images
                .Where(image => image.HotelId == hotelId)
                .CountAsync();
        }
    }
}
