using System.Linq.Expressions;
using FluentValidation;
using HotelBooking.Domain.Abstractions.Repositories.Room;
using HotelBooking.Domain.Abstractions.Services.Room;
using HotelBooking.Domain.Constants;
using HotelBooking.Domain.Exceptions;
using HotelBooking.Domain.Extensions;
using HotelBooking.Domain.Models;
using HotelBooking.Domain.Models.Room;
using SixLabors.ImageSharp;

namespace HotelBooking.Application.Services.Room
{
    /// <inheritdoc cref="IRoomAdminService"/>
    internal class RoomAdminService : IRoomAdminService
    {
        private readonly IValidator<PaginationDTO> _paginationValidator;
        private readonly IRoomService _roomService;
        private readonly IRoomImageService _roomImageService;
        private readonly IRoomAdminRepository _roomAdminRepository;

        public RoomAdminService(
            IValidator<PaginationDTO> paginationValidator,
            IRoomService roomService,
            IRoomImageService roomImageService,
            IRoomAdminRepository roomAdminRepository)
        {
            _paginationValidator = paginationValidator;
            _roomService = roomService;
            _roomImageService = roomImageService;
            _roomAdminRepository = roomAdminRepository;
        }

        public async Task<IEnumerable<RoomForAdminDTO>> GetByPageAsync(
            PaginationDTO pagination)
        {
            await _paginationValidator.ValidateAndThrowAsync(pagination);

            return _roomAdminRepository.GetByPage(
                (pagination.PageNumber - 1) * pagination.PageSize,
                pagination.PageSize);
        }

        public async Task<IEnumerable<RoomForAdminDTO>> SearchByPageAsync(
            PaginationDTO pagination, string searchQuery)
        {
            await _paginationValidator.ValidateAndThrowAsync(pagination);

            return _roomAdminRepository.SearchByPage(
                (pagination.PageNumber - 1) * pagination.PageSize,
                pagination.PageSize,
                ToSearchExpression(searchQuery));
        }

        private Expression<Func<RoomForAdminDTO, bool>> ToSearchExpression(string searchQuery)
        {
            searchQuery = searchQuery.ToComparableSearchQuery();

            return room =>
                room.Type.ToLower().Contains(searchQuery) ||
                room.Number.ToString().Contains(searchQuery);
        }

        public async Task AddImagesAsync(Guid roomId, IEnumerable<Image> images)
        {
            await _roomService.ValidateIdAsync(roomId);
            await ValidateNumberOfImagesForRoomAsync(roomId, images.Count());

            await _roomImageService.AddAsync(roomId, images);
        }

        private async Task ValidateNumberOfImagesForRoomAsync(Guid roomId, int numberOfImagesToAdd)
        {
            var numberOfStoredImages = await _roomImageService.GetCountAsync(roomId);

            if (numberOfStoredImages + numberOfImagesToAdd >
                ImagesConstants.MaxNumberOfImagesPerEntity)
                throw new EntityImagesLimitExceededException();
        }

        public Task<int> GetSearchCountAsync(string searchQuery) =>
            _roomAdminRepository.GetSearchCountAsync(ToSearchExpression(searchQuery));
    }
}
