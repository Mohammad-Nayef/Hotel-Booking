using FluentValidation;
using HotelBooking.Api.Extensions;
using HotelBooking.Domain.Abstractions.Services.City;
using HotelBooking.Domain.Abstractions.Services.Hotel;
using HotelBooking.Domain.Abstractions.Services.Room;
using HotelBooking.Domain.Constants;
using HotelBooking.Domain.Models.Image;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SixLabors.ImageSharp;

namespace HotelBooking.Api.Controllers
{
    [Authorize(Roles = $"{UserRoles.Admin}, {UserRoles.RegularUser}")]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    [ApiController]
    [Route("api")]
    public class ImageRetrievingController : Controller
    {
        private readonly IHotelImageService _hotelImageService;
        private readonly ICityImageService _cityImageService;
        private readonly IRoomImageService _roomImageService;

        public ImageRetrievingController(
            IHotelImageService hotelImageService,
            ICityImageService cityImageService,
            IRoomImageService roomImageService)
        {
            _hotelImageService = hotelImageService;
            _cityImageService = cityImageService;
            _roomImageService = roomImageService;
        }

        /// <summary>
        /// Gets an image for a city by an image Id.
        /// Use 0 pixels for a single dimension to obtain the original aspect ratio of the image.
        /// </summary>
        /// <param name="imageId">Id of the image to return.</param>
        ///<response code="200">The image is returned successfully.</response>
        [HttpGet("cities/images/{imageId}")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public Task<IActionResult> GetCityImageAsync(
            Guid imageId, [FromQuery] ImageSizeDTO imageSize) =>
            GetEntityImageAsync(imageId, imageSize, _cityImageService.GetImageAsync);

        /// <summary>
        /// Gets an image for a hotel by an image Id.
        /// Use 0 pixels for a single dimension to obtain the original aspect ratio of the image.
        /// </summary>
        /// <param name="imageId">Id of the image to return.</param>
        ///<response code="200">The image is returned successfully.</response>
        [HttpGet("hotels/images/{imageId}")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public Task<IActionResult> GetHotelImageAsync(
            Guid imageId, [FromQuery] ImageSizeDTO imageSize) =>
            GetEntityImageAsync(imageId, imageSize, _hotelImageService.GetImageAsync);

        /// <summary>
        /// Gets an image for a room by an image Id. 
        /// Use 0 pixels for a single dimension to obtain the original aspect ratio of the image.
        /// </summary>
        /// <param name="imageId">Id of the image to return.</param>
        ///<response code="200">The image is returned successfully.</response>
        [HttpGet("rooms/images/{imageId}")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public Task<IActionResult> GetRoomImageAsync(
            Guid imageId, [FromQuery] ImageSizeDTO imageSize) =>
            GetEntityImageAsync(imageId, imageSize, _roomImageService.GetImageAsync);

        private async Task<IActionResult> GetEntityImageAsync(
            Guid imageId,
            ImageSizeDTO imageSize,
            Func<Guid, ImageSizeDTO, Task<Stream>> entityImageGetterAsync)
        {
            Stream imageStream;

            try
            {
                imageStream = await entityImageGetterAsync(imageId, imageSize);
            }
            catch (ValidationException ex)
            {
                return BadRequest(ex.GetErrorsForClient());
            }
            catch (KeyNotFoundException)
            {
                return NotFound();
            }

            var imageFormat = await Image.DetectFormatAsync(imageStream);

            return File(imageStream, imageFormat.DefaultMimeType);
        }

        /// <summary>
        /// Get list of images IDs for a city.
        /// </summary>
        /// <param name="cityId">Id of the city.</param>
        [HttpGet("cities/{cityId}/imagesIds")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(IEnumerable<Guid>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetCityImagesIdsAsync(Guid cityId)
        {
            IEnumerable<Guid> imagesIds;

            try
            {
                imagesIds = await _cityImageService.GetImagesIdsAsync(cityId);
            }
            catch (KeyNotFoundException)
            {
                return NotFound();
            }

            return Ok(imagesIds);
        }

        /// <summary>
        /// Get list of images IDs for an hotel.
        /// </summary>
        /// <param name="hotelId">Id of the hotel.</param>
        [HttpGet("hotels/{hotelId}/imagesIds")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(IEnumerable<Guid>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetHotelImagesIdsAsync(Guid hotelId)
        {
            IEnumerable<Guid> imagesIds;

            try
            {
                imagesIds = await _hotelImageService.GetImagesIdsAsync(hotelId);
            }
            catch (KeyNotFoundException)
            {
                return NotFound();
            }

            return Ok(imagesIds);
        }

        /// <summary>
        /// Get list of images IDs for a room.
        /// </summary>
        /// <param name="roomId">Id of the room.</param>
        [HttpGet("rooms/{roomId}/imagesIds")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(IEnumerable<Guid>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetRoomImagesIdsAsync(Guid roomId)
        {
            IEnumerable<Guid> imagesIds;

            try
            {
                imagesIds = await _roomImageService.GetImagesIdsAsync(roomId);
            }
            catch (KeyNotFoundException)
            {
                return NotFound();
            }

            return Ok(imagesIds);
        }
    }
}
