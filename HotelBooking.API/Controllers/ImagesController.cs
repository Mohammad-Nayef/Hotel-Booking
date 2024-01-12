using HotelBooking.Domain.Abstractions.Services;
using Microsoft.AspNetCore.Mvc;
using SixLabors.ImageSharp;

namespace HotelBooking.Api.Controllers
{
    [ApiController]
    [Route("api")]
    public class ImagesController : Controller
    {
        private readonly IImageService _imageService;

        public ImagesController(IImageService imageService)
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
        public async Task<IActionResult> GetCityImageAsync(Guid imageId)
        {
            FileStream imageStream;

            try
            {
                imageStream = await _imageService.GetCityImageAsync(imageId);
            }
            catch (KeyNotFoundException) 
            {
                return NotFound();
            }

            var imageFormat = await Image.DetectFormatAsync(imageStream);

            return File(imageStream, imageFormat.DefaultMimeType);
        }

        /// <summary>
        /// Gets an image for a hotel by an image Id.
        /// </summary>
        /// <param name="imageId">Id of the image to return.</param>
        ///<response code="200">The image is returned successfully.</response>
        [HttpGet("hotels/images/{imageId}")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetHotelImageAsync(Guid imageId)
        {
            FileStream imageStream;

            try
            {
                imageStream = await _imageService.GetHotelImageAsync(imageId);
            }
            catch (KeyNotFoundException)
            {
                return NotFound();
            }

            var imageFormat = await Image.DetectFormatAsync(imageStream);

            return File(imageStream, imageFormat.DefaultMimeType);
        }

        /// <summary>
        /// Gets an image for a room by an image Id.
        /// </summary>
        /// <param name="imageId">Id of the image to return.</param>
        ///<response code="200">The image is returned successfully.</response>
        [HttpGet("rooms/images/{imageId}")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetRoomImageAsync(Guid imageId)
        {
            FileStream imageStream;

            try
            {
                imageStream = await _imageService.GetRoomImageAsync(imageId);
            }
            catch (KeyNotFoundException)
            {
                return NotFound();
            }

            var imageFormat = await Image.DetectFormatAsync(imageStream);

            return File(imageStream, imageFormat.DefaultMimeType);
        }

        /// <summary>
        /// Gets a thumbnail of an image for a room by an image Id.
        /// </summary>
        /// <param name="thumbnailId">Id of the image thumbnail to return.</param>
        ///<response code="200">The thumbnail is returned successfully.</response>
        [HttpGet("rooms/thumbnails/{thumbnailId}")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetThumbnailOfRoomImageAsync(Guid thumbnailId)
        {
            FileStream thumbnailStream;

            try
            {
                thumbnailStream = await _imageService.GetThumbnailOfRoomImageAsync(thumbnailId);
            }
            catch (KeyNotFoundException)
            {
                return NotFound();
            }

            var thumbnailFormat = await Image.DetectFormatAsync(thumbnailStream);

            return File(thumbnailStream, thumbnailFormat.DefaultMimeType);
        }

        /// <summary>
        /// Gets a thumbnail of an image for a hotel by a thumbnail Id.
        /// </summary>
        /// <param name="thumbnailId">Id of the image thumbnail to return.</param>
        ///<response code="200">The thumbnail is returned successfully.</response>
        [HttpGet("hotels/thumbnails/{thumbnailId}")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetThumbnailOfHotelImageAsync(Guid thumbnailId)
        {
            FileStream thumbnailStream;

            try
            {
                thumbnailStream = await _imageService.GetThumbnailOfHotelImageAsync(thumbnailId);
            }
            catch (KeyNotFoundException)
            {
                return NotFound();
            }

            var thumbnailFormat = await Image.DetectFormatAsync(thumbnailStream);

            return File(thumbnailStream, thumbnailFormat.DefaultMimeType);
        }

        /// <summary>
        /// Gets a thumbnail of an image for a city by a thumbnail Id.
        /// </summary>
        /// <param name="thumbnailId">Id of the image thumbnail to return.</param>
        ///<response code="200">The thumbnail is returned successfully.</response>
        [HttpGet("cities/thumbnails/{thumbnailId}")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetThumbnailOfCityImageAsync(Guid thumbnailId)
        {
            FileStream thumbnailStream;

            try
            {
                thumbnailStream = await _imageService.GetThumbnailOfCityImageAsync(thumbnailId);
            }
            catch (KeyNotFoundException)
            {
                return NotFound();
            }

            var thumbnailFormat = await Image.DetectFormatAsync(thumbnailStream);

            return File(thumbnailStream, thumbnailFormat.DefaultMimeType);
        }
    }
}
