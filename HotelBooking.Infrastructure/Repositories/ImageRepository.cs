using HotelBooking.Domain.Abstractions.Repositories;
using HotelBooking.Infrastructure.Tables;
using Microsoft.EntityFrameworkCore;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Processing;

namespace HotelBooking.Infrastructure.Repositories
{
    /// <inheritdoc cref="IImageRepository"/>
    internal class ImageRepository : IImageRepository
    {
        private readonly string _mainDirectory = $"{AppDomain.CurrentDomain.BaseDirectory}\\Images";
        private readonly HotelsBookingDbContext _dbContext;
        private const string ThumbnailsFolder = "Thumbnails";
        private const int MinThumbnailLength = 200;
        private const string Extension = "jpeg";

        public ImageRepository(HotelsBookingDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public Task<int> GetCountAsync(Guid entityId)
        {
            return _dbContext.Images
                .Where(image => image.EntityId == entityId)
                .CountAsync();
        }

        public async Task AddRangeAsync(Guid entityId, IEnumerable<Image> images)
        {
            var imagesTable = new List<ImageTable>(images.Count());

            images.ToList().ForEach(image =>
            {
                GenerateAndStoreImage(
                    image, out var imageId, out var imagePath, out var thumbnailPath);

                var imageTable = new ImageTable
                {
                    Id = imageId,
                    Path = imagePath,
                    ThumbnailPath = thumbnailPath,
                    EntityId = entityId
                };

                imagesTable.Add(imageTable);
            });

            await PersistAsync(imagesTable);
        }

        public async Task<bool> ExistsAsync(Guid imageId)
        {
            return await ExistsInDatabaseAsync(imageId) &&
                ExistsInFileSystem(imageId.ToString());
        }

        public async Task<bool> ThumbnailExistsAsync(Guid thumbnailId)
        {
            return await ExistsInDatabaseAsync(thumbnailId) &&
                ThumbnailExistsInFileSystem(thumbnailId.ToString());
        }

        public FileStream Get(Guid imageId) =>
            new FileStream($"{_mainDirectory}\\{imageId}.{Extension}", FileMode.Open);

        public FileStream GetThumbnail(Guid thumbnailId)
        {
            return new FileStream(
                $"{_mainDirectory}\\{ThumbnailsFolder}\\{thumbnailId}.{Extension}",
                FileMode.Open);
        }

        private void StoreImageAndThumbnail(Image image, string imagePath, string thumbnailPath)
        {
            image.Save(imagePath);
            MakeThumbnail(image, out var thumbnail);
            thumbnail.Save(thumbnailPath);
        }

        private async Task PersistAsync(List<ImageTable> imagesTable)
        {
            await _dbContext.Images.AddRangeAsync(imagesTable);
            await _dbContext.SaveChangesAsync();
        }

        private void GenerateAndStoreImage(
            Image image,
            out Guid imageId,
            out string imagePath,
            out string thumbnailPath)
        {
            imageId = Guid.NewGuid();
            (imagePath, thumbnailPath) = GetFullPaths(image, imageId);
            StoreImageAndThumbnail(image, imagePath, thumbnailPath);
        }

        private bool ThumbnailExistsInFileSystem(string thumbnailName)
        {
            return File.Exists(
                $"{_mainDirectory}\\{ThumbnailsFolder}\\{thumbnailName}" +
                $".{Extension}");
        }

        private bool ExistsInFileSystem(string imageName) =>
            File.Exists($"{_mainDirectory}\\{imageName}.{Extension}");

        private Task<bool> ExistsInDatabaseAsync(Guid imageId) =>
            _dbContext.Images.AnyAsync(image => image.Id == imageId);

        private (string, string) GetFullPaths(Image image, Guid imageId)
        {
            Directory.CreateDirectory($"{_mainDirectory}");
            var imagePath =
                $"{_mainDirectory}\\{imageId}.{Extension}";

            Directory.CreateDirectory($"{_mainDirectory}\\{ThumbnailsFolder}");
            var thumbnailPath = $"{_mainDirectory}\\{ThumbnailsFolder}\\{imageId}." +
                Extension;

            return (imagePath, thumbnailPath);
        }

        private Image MakeThumbnail(Image image, out Image thumbnail)
        {
            thumbnail = image;
            ReduceHeightAndWidth(thumbnail);

            return thumbnail;
        }

        private static void ReduceHeightAndWidth(Image thumbnail)
        {
            // 0 is used to maintain the aspect ratio of the image.
            if (thumbnail.Width < thumbnail.Height)
                thumbnail.Mutate(thumbnail => thumbnail.Resize(MinThumbnailLength, 0));
            else
                thumbnail.Mutate(thumbnail => thumbnail.Resize(0, MinThumbnailLength));
        }

        public IEnumerable<Guid> GetImagesIds(Guid entityId)
        {
            return _dbContext.Images
                .Where(image => image.EntityId == entityId)
                .Select(image => image.Id)
                .AsEnumerable();
        }
    }
}
