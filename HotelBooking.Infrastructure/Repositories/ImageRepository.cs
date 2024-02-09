using HotelBooking.Domain.Abstractions.Repositories;
using HotelBooking.Domain.Models.Image;
using HotelBooking.Infrastructure.Tables;
using Microsoft.EntityFrameworkCore;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Processing;

namespace HotelBooking.Infrastructure.Repositories
{
    /// <inheritdoc cref="IImageRepository"/>
    internal class ImageRepository : IImageRepository
    {
        private readonly string _mainDirectory =
            $"{AppDomain.CurrentDomain.BaseDirectory}\\Images";
        private readonly HotelsBookingDbContext _dbContext;
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
                    image, out var imageId, out var imagePath);

                var imageTable = new ImageTable
                {
                    Id = imageId,
                    Path = imagePath,
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

        public Task<Stream> GetAsync(Guid imageId, ImageSizeDTO imageSize)
        {
            var imagePath = $"{_mainDirectory}\\{imageId}.{Extension}";

            return GetTemporaryImageStreamAsync(imageId, imagePath, imageSize);
        }

        private async Task<Stream> GetTemporaryImageStreamAsync(
            Guid imageId, string imagePath, ImageSizeDTO imageSize)
        {
            var temporaryPath = $"{_mainDirectory}\\Temp - {imageId}.{Extension}";
            using var image = (await Image.LoadAsync(imagePath))
                .Clone(image => image.Resize(imageSize.Width, imageSize.Height));
            await image.SaveAsync(temporaryPath);
            var imageBytes = await File.ReadAllBytesAsync(temporaryPath);
            var imageMemoryStream = new MemoryStream(imageBytes);
            File.Delete(temporaryPath);

            return imageMemoryStream;
        }

        private async Task PersistAsync(List<ImageTable> imagesTable)
        {
            await _dbContext.Images.AddRangeAsync(imagesTable);
            await _dbContext.SaveChangesAsync();
        }

        private void GenerateAndStoreImage(
            Image image,
            out Guid imageId,
            out string imagePath)
        {
            imageId = Guid.NewGuid();
            imagePath = GetFullPaths(imageId);
            image.Save(imagePath);
        }

        private bool ExistsInFileSystem(string imageName) =>
            File.Exists($"{_mainDirectory}\\{imageName}.{Extension}");

        private Task<bool> ExistsInDatabaseAsync(Guid imageId) =>
            _dbContext.Images.AnyAsync(image => image.Id == imageId);

        private string GetFullPaths(Guid imageId)
        {
            Directory.CreateDirectory($"{_mainDirectory}");

            return $"{_mainDirectory}\\{imageId}.{Extension}";
        }

        public IEnumerable<Guid> GetImagesIds(Guid entityId)
        {
            return _dbContext.Images
                .Where(image => image.EntityId == entityId)
                .Select(image => image.Id)
                .AsEnumerable()
                .Where(imageId => ExistsInFileSystem(imageId.ToString()));
        }
    }
}
