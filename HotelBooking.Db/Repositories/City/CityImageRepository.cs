using HotelBooking.Db.Tables;
using HotelBooking.Domain.Abstractions.Repositories.City;
using Microsoft.EntityFrameworkCore;

namespace HotelBooking.Db.Repositories.City
{
    internal class CityImageRepository : ImageRepository, ICityImageRepository
    {
        private readonly HotelsBookingDbContext _dbContext;

        public CityImageRepository(HotelsBookingDbContext dbContext) : base(dbContext, "Cities")
        {
            _dbContext = dbContext;
        }

        protected override ImageTable CreateImageTable(
            Guid entityId, Guid imageId, string imagePath, string thumbnailPath)
        {
            return new ImageTable
            {
                Id = imageId,
                CityId = entityId,
                Path = imagePath,
                ThumbnailPath = thumbnailPath
            };
        }

        public override Task<int> GetCountAsync(Guid cityId)
        {
            return _dbContext.Images
                .Where(image => image.CityId == cityId)
                .CountAsync();
        }
    }
}
