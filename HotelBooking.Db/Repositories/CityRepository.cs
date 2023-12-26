using System.Linq.Expressions;
using AutoMapper;
using HotelBooking.Db.Tables;
using HotelBooking.Domain.Abstractions.Repositories;
using HotelBooking.Domain.Models;
using Microsoft.EntityFrameworkCore;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Processing;

namespace HotelBooking.Db.Repositories
{
    internal class CityRepository : ICityRepository
    {
        private readonly HotelsBookingDbContext _dbContext;
        private readonly IMapper _mapper;
        private const string ImagesPath = "CitiesImages";
        private const string ThumbnailsPath = "CitiesImages\\Thumbnails";

        public CityRepository(HotelsBookingDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public async Task<Guid> AddAsync(CityDTO newCity)
        {
            var entityEntry = await _dbContext.Cities.AddAsync(_mapper.Map<CityTable>(newCity));
            await _dbContext.SaveChangesAsync();

            return entityEntry.Entity.Id;
        }

        public async Task DeleteAsync(Guid id)
        {
            _dbContext.Cities.Remove(new CityTable { Id = id });
            await _dbContext.SaveChangesAsync();
        }

        public Task<bool> ExistsAsync(Guid id) =>
            _dbContext.Cities.AnyAsync(city => city.Id == id);

        public async Task<CityDTO> GetByIdAsync(Guid id) =>
            _mapper.Map<CityDTO>(await _dbContext.Cities.AsNoTracking().FirstOrDefaultAsync());

        public async Task<int> GetCountAsync()
        {
            if (_dbContext.Cities.TryGetNonEnumeratedCount(out var count))
                return count;

            return await _dbContext.Cities.CountAsync();
        }

        public IEnumerable<CityForAdminDTO> GetForAdminByPage(int itemsToSkip, int itemsToTake)
        {
            return _dbContext.CitiesForAdmin
                .OrderBy(city => city.Name)
                .Skip(itemsToSkip)
                .Take(itemsToTake)
                .AsNoTracking()
                .AsEnumerable();
        }

        public IEnumerable<CityForAdminDTO> SearchByCityForAdminByPage(
            int itemsToSkip,
            int itemsToTake,
            Expression<Func<CityForAdminDTO, bool>> searchExpression)
        {
            return _dbContext.CitiesForAdmin
                .Where(searchExpression)
                .OrderBy(city => city.Name)
                .Skip(itemsToSkip)
                .Take(itemsToTake)
                .AsNoTracking()
                .AsEnumerable();
        }

        public Task<int> GetSearchByCityForAdminCountAsync(
            Expression<Func<CityForAdminDTO, bool>> searchExpression)
        {
            return _dbContext.CitiesForAdmin
                .Where(searchExpression)
                .CountAsync();
        }

        public async Task UpdateAsync(CityDTO city)
        {
            _dbContext.Cities.Update(_mapper.Map<CityTable>(city));
            await _dbContext.SaveChangesAsync();
        }

        public async Task AddImagesAsync(Guid cityId, IEnumerable<Image> images)
        {
            var imagesTable = new List<ImageTable>();

            images.ToList().ForEach(image =>
            {
                string currentDirectory = AppDomain.CurrentDomain.BaseDirectory;

                var imageId = Guid.NewGuid();
                var imagePath = 
                    $"{currentDirectory}{ImagesPath}\\{imageId}." +
                    $"{image.Metadata.DecodedImageFormat.Name}";
                var thumbnailPath = 
                    $"{currentDirectory}{ThumbnailsPath}\\{imageId}." +
                    $"{image.Metadata.DecodedImageFormat.Name}";

                Directory.CreateDirectory($"{currentDirectory}{ImagesPath}");
                image.Save(imagePath);
                
                var thumbnail = image;
                if (thumbnail.Width < thumbnail.Height)
                    thumbnail.Mutate(thumbnail => thumbnail.Resize(200, 0));
                else
                    thumbnail.Mutate(thumbnail => thumbnail.Resize(0, 200));

                Directory.CreateDirectory($"{currentDirectory}{ThumbnailsPath}");
                thumbnail.Save(thumbnailPath);

                imagesTable.Add(new ImageTable
                {
                    Id = imageId,
                    CityId = cityId,
                    Path = imagePath,
                    ThumbnailPath = thumbnailPath
                });
            });

            await _dbContext.Images.AddRangeAsync(imagesTable);
            await _dbContext.SaveChangesAsync();
        }

        public Task<int> GetNumberOfImagesAsync(Guid cityId) => 
            _dbContext.Images.Where(image => image.CityId == cityId).CountAsync();
    }
}
