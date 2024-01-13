using HotelBooking.Domain.Abstractions.Services;
using HotelBooking.Domain.Constants;
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
        private readonly IImageService _imageService;

        public ImageRetrievingController(IImageService imageService)
        {
            _imageService = imageService;
        }

        /// <summary>
        /// Gets an image for a city by an image Id.
        /// </summary>
        /// <param name="imageId">Id of the image to return.</param>
        ///<response code="200">The image is returned successfully.</response>
        [HttpGet("cities/images/{imageId}")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public Task<IActionResult> GetCityImageAsync(Guid imageId) =>
            GetEntityImageAsync(imageId, _imageService.GetCityImageAsync);

        /// <summary>
        /// Gets an image for a hotel by an image Id.
        /// </summary>
        /// <param name="imageId">Id of the image to return.</param>
        ///<response code="200">The image is returned successfully.</response>
        [HttpGet("hotels/images/{imageId}")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public Task<IActionResult> GetHotelImageAsync(Guid imageId) =>
            GetEntityImageAsync(imageId, _imageService.GetHotelImageAsync);

        /// <summary>
        /// Gets an image for a room by an image Id.
        /// </summary>
        /// <param name="imageId">Id of the image to return.</param>
        ///<response code="200">The image is returned successfully.</response>
        [HttpGet("rooms/images/{imageId}")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public Task<IActionResult> GetRoomImageAsync(Guid imageId) =>
            GetEntityImageAsync(imageId, _imageService.GetRoomImageAsync);

        /// <summary>
        /// Gets a thumbnail of an image for a room by an image Id.
        /// </summary>
        /// <param name="thumbnailId">Id of the image thumbnail to return.</param>
        ///<response code="200">The thumbnail is returned successfully.</response>
        [HttpGet("rooms/thumbnails/{thumbnailId}")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public Task<IActionResult> GetThumbnailOfRoomImageAsync(Guid thumbnailId) =>
            GetEntityImageAsync(thumbnailId, _imageService.GetThumbnailOfRoomImageAsync);

        /// <summary>
        /// Gets a thumbnail of an image for a hotel by a thumbnail Id.
        /// </summary>
        /// <param name="thumbnailId">Id of the image thumbnail to return.</param>
        ///<response code="200">The thumbnail is returned successfully.</response>
        [HttpGet("hotels/thumbnails/{thumbnailId}")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public Task<IActionResult> GetThumbnailOfHotelImageAsync(Guid thumbnailId) =>
            GetEntityImageAsync(thumbnailId, _imageService.GetThumbnailOfHotelImageAsync);

        /// <summary>
        /// Gets a thumbnail of an image for a city by a thumbnail Id.
        /// </summary>
        /// <param name="thumbnailId">Id of the image thumbnail to return.</param>
        ///<response code="200">The thumbnail is returned successfully.</response>
        [HttpGet("cities/thumbnails/{thumbnailId}")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public Task<IActionResult> GetThumbnailOfCityImageAsync(Guid thumbnailId) =>
            GetEntityImageAsync(thumbnailId, _imageService.GetThumbnailOfCityImageAsync);

        private async Task<IActionResult> GetEntityImageAsync(
            Guid imageId, Func<Guid, Task<FileStream>> entityImageGetterAsync)
        {
            FileStream imageStream;

            try
            {
                imageStream = await entityImageGetterAsync(imageId);
            }
            catch (KeyNotFoundException)
            {
                return NotFound();
            }

            var imageFormat = await Image.DetectFormatAsync(imageStream);

            return File(imageStream, imageFormat.DefaultMimeType);
        }
    }
}
