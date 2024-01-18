using HotelBooking.Domain.Abstractions.Repositories.Room;
using HotelBooking.Infrastructure.Tables;
using Microsoft.EntityFrameworkCore;

namespace HotelBooking.Infrastructure.Repositories.Room
{
    internal class RoomImageRepository : ImageRepository, IRoomImageRepository
    {
        private readonly HotelsBookingDbContext _dbContext;

        public RoomImageRepository(HotelsBookingDbContext dbContext) : base(dbContext, "Rooms")
        {
            _dbContext = dbContext;
        }

        protected override ImageTable CreateImageTable(
            Guid entityId, Guid imageId, string imagePath, string thumbnailPath)
        {
            return new ImageTable
            {
                Id = imageId,
                RoomId = entityId,
                Path = imagePath,
                ThumbnailPath = thumbnailPath
            };
        }

        public override Task<int> GetCountAsync(Guid roomId)
        {
            return _dbContext.Images
                .Where(image => image.RoomId == roomId)
                .CountAsync();
        }
    }
}
