﻿using HotelBooking.Db.Tables;
using HotelBooking.Domain.Abstractions.Repositories;
using Microsoft.EntityFrameworkCore;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Processing;

namespace HotelBooking.Db.Repositories
{
    internal class ImageRepository : IImageRepository
    {
        private readonly string _mainDirectory = $"{AppDomain.CurrentDomain.BaseDirectory}\\Images";
        private readonly HotelsBookingDbContext _dbContext;
        private const string ThumbnailsPath = "Thumbnails";
        private const string CitiesPath = "Cities";
        private const string HotelsPath = "Hotels";
        private const string RoomsPath = "Rooms";
        private const int MinThumbnailLength = 200;
        private const string _extension = "jpeg";

        public ImageRepository(HotelsBookingDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task AddForCityAsync(Guid cityId, IEnumerable<Image> images)
        {
            var imagesTable = new List<ImageTable>();

            images.ToList().ForEach(image =>
            {
                GenerateImage(
                    image, CitiesPath, out var imageId, out var imagePath, out var thumbnailPath);

                imagesTable.Add(new ImageTable
                {
                    Id = imageId,
                    CityId = cityId,
                    Path = imagePath,
                    ThumbnailPath = thumbnailPath
                });
            });

            await PersistAsync(imagesTable);
        }

        private void GenerateImage(
            Image image,
            string entityPath,
            out Guid imageId,
            out string imagePath,
            out string thumbnailPath)
        {
            imageId = Guid.NewGuid();
            (imagePath, thumbnailPath) = GetFullPaths(entityPath, image, imageId);
            StoreImageAndThumbnail(image, imagePath, thumbnailPath);
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

        private (string, string) GetFullPaths(string entityPath, Image image, Guid imageId)
        {
            Directory.CreateDirectory($"{_mainDirectory}\\{entityPath}");
            var imagePath =
                $"{_mainDirectory}\\{entityPath}\\{imageId}.{_extension}";

            Directory.CreateDirectory($"{_mainDirectory}\\{entityPath}\\{ThumbnailsPath}");
            var thumbnailPath = $"{_mainDirectory}\\{entityPath}\\{ThumbnailsPath}\\{imageId}." +
                _extension;

            return (imagePath, thumbnailPath);
        }

        private Image MakeThumbnail(Image image, out Image thumbnail)
        {
            thumbnail = image;

            // 0 is used to maintain the aspect ration of the image.
            if (thumbnail.Width < thumbnail.Height)
                thumbnail.Mutate(thumbnail => thumbnail.Resize(MinThumbnailLength, 0));
            else
                thumbnail.Mutate(thumbnail => thumbnail.Resize(0, MinThumbnailLength));

            return thumbnail;
        }

        public async Task AddForHotelAsync(Guid hotelId, IEnumerable<Image> images)
        {
            var imagesTable = new List<ImageTable>();

            images.ToList().ForEach(image =>
            {
                GenerateImage(
                    image, HotelsPath, out var imageId, out var imagePath, out var thumbnailPath);

                imagesTable.Add(new ImageTable
                {
                    Id = imageId,
                    HotelId = hotelId,
                    Path = imagePath,
                    ThumbnailPath = thumbnailPath
                });
            });

            await PersistAsync(imagesTable);
        }

        public async Task AddForRoomAsync(Guid roomId, IEnumerable<Image> images)
        {
            var imagesTable = new List<ImageTable>();

            images.ToList().ForEach(image =>
            {
                GenerateImage(
                    image, RoomsPath, out var imageId, out var imagePath, out var thumbnailPath);

                imagesTable.Add(new ImageTable
                {
                    Id = imageId,
                    RoomId = roomId,
                    Path = imagePath,
                    ThumbnailPath = thumbnailPath
                });
            });

            await PersistAsync(imagesTable);
        }

        public async Task<bool> HotelImageExistsAsync(Guid imageId) =>
            await ExistsInDatabaseAsync(imageId) && ExistsInFileSystem(HotelsPath, imageId.ToString());

        public async Task<bool> CityImageExistsAsync(Guid imageId)
        {
            return await ExistsInDatabaseAsync(imageId) && 
                ExistsInFileSystem(CitiesPath, imageId.ToString());
        }

        public async Task<bool> RoomImageExistsAsync(Guid imageId) =>
            await ExistsInDatabaseAsync(imageId) && ExistsInFileSystem(RoomsPath, imageId.ToString());

        private bool ExistsInFileSystem(string entityPath, string imageName) =>
            File.Exists($"{_mainDirectory}\\{entityPath}\\{imageName}.{_extension}");

        private Task<bool> ExistsInDatabaseAsync(Guid imageId) =>
            _dbContext.Images.AnyAsync(image => image.Id == imageId);

        public FileStream GetCityImage(Guid imageId)
        {
            return new FileStream(
                $"{_mainDirectory}\\{CitiesPath}\\{imageId}.{_extension}", FileMode.Open);
        }

        public FileStream GetHotelImage(Guid imageId)
        {
            return new FileStream(
                $"{_mainDirectory}\\{HotelsPath}\\{imageId}.{_extension}", FileMode.Open);
        }

        public FileStream GetRoomImage(Guid imageId)
        {
            return new FileStream(
                $"{_mainDirectory}\\{RoomsPath}\\{imageId}.{_extension}", FileMode.Open);
        }
    }
}
