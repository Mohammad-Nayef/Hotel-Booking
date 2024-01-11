using System.Linq.Expressions;
using FluentValidation;
using HotelBooking.Domain.Abstractions.Repositories;
using HotelBooking.Domain.Abstractions.Services;
using HotelBooking.Domain.Constants;
using HotelBooking.Domain.Exceptions;
using HotelBooking.Domain.Models;
using SixLabors.ImageSharp;

namespace HotelBooking.Application.Services
{
    internal class RoomService : IRoomService
    {
        private readonly IRoomRepository _roomRepository;
        private readonly IValidator<PaginationDTO> _paginationValidator;
        private readonly IValidator<RoomDTO> _roomValidator;
        private readonly IImageService _imageService;

        public RoomService(
            IRoomRepository roomRepository,
            IValidator<PaginationDTO> paginationValidator,
            IValidator<RoomDTO> roomValidator,
            IImageService imageService)
        {
            _roomRepository = roomRepository;
            _paginationValidator = paginationValidator;
            _roomValidator = roomValidator;
            _imageService = imageService;
        }

        public async Task<Guid> AddAsync(RoomDTO newRoom)
        {
            await _roomValidator.ValidateAndThrowAsync(newRoom);

            newRoom.CreationDate = DateTime.UtcNow;
            newRoom.ModificationDate = DateTime.UtcNow;

            return await _roomRepository.AddAsync(newRoom);
        }

        public async Task DeleteAsync(Guid id)
        {
            await ValidateIdAsync(id);

            await _roomRepository.DeleteAsync(id);
        }

        private async Task ValidateIdAsync(Guid id)
        {
            if (!await ExistsAsync(id))
                throw new KeyNotFoundException($"The Id '{id}' does not exist.");
        }

        public Task<bool> ExistsAsync(Guid id) => _roomRepository.ExistsAsync(id);

        public async Task<RoomDTO> GetByIdAsync(Guid id)
        {
            await ValidateIdAsync(id);

            return await _roomRepository.GetByIdAsync(id);
        }

        public Task<int> GetCountAsync() => _roomRepository.GetCountAsync();

        public async Task<IEnumerable<RoomForAdminDTO>> GetForAdminByPageAsync(
            PaginationDTO pagination)
        {
            await _paginationValidator.ValidateAndThrowAsync(pagination);

            return _roomRepository.GetForAdminByPage(
                (pagination.PageNumber - 1) * pagination.PageSize,
                pagination.PageSize);
        }

        public async Task UpdateAsync(RoomDTO room)
        {
            await _roomValidator.ValidateAndThrowAsync(room);

            room.ModificationDate = DateTime.UtcNow;

            await _roomRepository.UpdateAsync(room);
        }

        public async Task<IEnumerable<RoomForAdminDTO>> SearchByRoomForAdminByPageAsync(
            PaginationDTO pagination, string searchQuery)
        {
            await _paginationValidator.ValidateAndThrowAsync(pagination);

            return _roomRepository.SearchByRoomForAdminByPage(
                (pagination.PageNumber - 1) * pagination.PageSize,
                pagination.PageSize,
                ToSearchExpression(searchQuery));
        }

        private Expression<Func<RoomForAdminDTO, bool>> ToSearchExpression(string searchQuery) =>
            room =>
                room.Type.Contains(searchQuery, StringComparison.CurrentCultureIgnoreCase) ||
                room.Number.ToString().Contains(searchQuery);

        public Task<int> GetSearchByRoomForAdminCountAsync(string searchQuery) =>
            _roomRepository.GetSearchByRoomForAdminCountAsync(ToSearchExpression(searchQuery));

        public async Task AddImagesForRoomAsync(Guid roomId, IEnumerable<Image> images)
        {
            await ValidateIdAsync(roomId);
            await ValidateNumberOfImagesForRoomAsync(roomId, images.Count());

            await _imageService.AddForRoomAsync(roomId, images);
        }

        private async Task ValidateNumberOfImagesForRoomAsync(Guid roomId, int numberOfImagesToAdd)
        {
            var numberOfStoredImages = await _roomRepository.GetNumberOfImagesAsync(roomId);

            if (numberOfStoredImages + numberOfImagesToAdd > 
                ImagesConstants.MaxNumberOfImagesPerEntity)
                throw new EntityImagesLimitExceededException();
        }
    }
}
