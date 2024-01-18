using HotelBooking.Domain.Abstractions.Repositories;
using HotelBooking.Infrastructure.Tables;
using Microsoft.EntityFrameworkCore;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Processing;

namespace HotelBooking.Infrastructure.Repositories
{
    internal abstract class ImageRepository : IImageRepository
    {
        private readonly string _mainDirectory = $"{AppDomain.CurrentDomain.BaseDirectory}\\Images";
        private readonly HotelsBookingDbContext _dbContext;
        private const string ThumbnailsFolder = "Thumbnails";
        private const int MinThumbnailLength = 200;
        private const string Extension = "jpeg";
        private readonly string _entityFolder;

        public ImageRepository(HotelsBookingDbContext dbContext, string entityFolderName)
        {
            _dbContext = dbContext;
            _entityFolder = entityFolderName;
        }

        public abstract Task<int> GetCountAsync(Guid entityId);

        public async Task AddRangeAsync(Guid entityId, IEnumerable<Image> images)
        {
            var imagesTable = new List<ImageTable>(images.Count());

            images.ToList().ForEach(image =>
            {
                GenerateImage(
                    image, out var imageId, out var imagePath, out var thumbnailPath);

                imagesTable.Add(CreateImageTable(entityId, imageId, imagePath, thumbnailPath));
            });

            await PersistAsync(imagesTable);
        }

        protected abstract ImageTable CreateImageTable(
            Guid entityId, Guid imageId, string imagePath, string thumbnailPath);

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

        public FileStream Get(Guid imageId)
        {
            return new FileStream(
                $"{_mainDirectory}\\{_entityFolder}\\{imageId}.{Extension}", FileMode.Open);
        }

        public FileStream GetThumbnail(Guid thumbnailId)
        {
            return new FileStream(
                $"{_mainDirectory}\\{_entityFolder}\\{ThumbnailsFolder}\\{thumbnailId}.{Extension}",
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

        private void GenerateImage(
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
                $"{_mainDirectory}\\{_entityFolder}\\{ThumbnailsFolder}\\{thumbnailName}" +
                $".{Extension}");
        }

        private bool ExistsInFileSystem(string imageName) =>
            File.Exists($"{_mainDirectory}\\{_entityFolder}\\{imageName}.{Extension}");

        private Task<bool> ExistsInDatabaseAsync(Guid imageId) =>
            _dbContext.Images.AnyAsync(image => image.Id == imageId);

        private (string, string) GetFullPaths(Image image, Guid imageId)
        {
            Directory.CreateDirectory($"{_mainDirectory}\\{_entityFolder}");
            var imagePath =
                $"{_mainDirectory}\\{_entityFolder}\\{imageId}.{Extension}";

            Directory.CreateDirectory($"{_mainDirectory}\\{_entityFolder}\\{ThumbnailsFolder}");
            var thumbnailPath = $"{_mainDirectory}\\{_entityFolder}\\{ThumbnailsFolder}\\{imageId}." +
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
            // 0 is used to maintain the aspect ration of the image.
            if (thumbnail.Width < thumbnail.Height)
                thumbnail.Mutate(thumbnail => thumbnail.Resize(MinThumbnailLength, 0));
            else
                thumbnail.Mutate(thumbnail => thumbnail.Resize(0, MinThumbnailLength));
        }
    }
}
