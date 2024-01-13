using FluentValidation;
using HotelBooking.Api.Extensions;
using HotelBooking.Domain.Abstractions.Services;
using HotelBooking.Domain.Constants;
using HotelBooking.Domain.Exceptions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SixLabors.ImageSharp;

namespace HotelBooking.Api.Controllers
{
    [Authorize(Roles = $"{UserRoles.Admin}")]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    [ApiController]
    [Route("api")]
    public class ImageCreationController : Controller
    {
        private readonly ICityService _cityService;
        private readonly IHotelService _hotelService;
        private readonly IRoomService _roomService;

        public ImageCreationController(
            ICityService cityService, IHotelService hotelService, IRoomService roomService)
        {
            _cityService = cityService;
            _hotelService = hotelService;
            _roomService = roomService;
        }

        /// <summary>
        /// Add images for a city.
        /// </summary>
        /// <param name="cityId">Id of the city to add images for.</param>        
        /// /// <response code="404">The city with the given Id doesn't exist.</response>
        /// <response code="204">The images are successfully.</response>
        [HttpPost("cities/{cityId}/images")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public Task<IActionResult> PostCityImagesAsync(
            Guid cityId, List<IFormFile> imagesForms)
        {
            return CreateEntityImageAsync(
                cityId, imagesForms, _cityService.AddImagesForCityAsync);
        }

        /// <summary>
        /// Add images for a hotel.
        /// </summary>
        /// <param name="hotelId">Id of the hotel to add images for.</param>        
        /// <response code="404">The hotel with the given Id doesn't exist.</response>
        /// <response code="204">The images are successfully added.</response>
        [HttpPost("hotels/{hotelId}/images")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public Task<IActionResult> PostHotelImagesAsync(
            Guid hotelId, List<IFormFile> imagesForms)
        {
            return CreateEntityImageAsync(
                hotelId, imagesForms, _hotelService.AddImagesForHotelAsync);
        }

        /// <summary>
        /// Add images for a room.
        /// </summary>
        /// <param name="roomId">Id of the room to add images for.</param>        
        /// <response code="404">The room with the given Id doesn't exist.</response>
        /// <response code="204">The images are successfully added.</response>
        [HttpPost("{roomId}/images")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public Task<IActionResult> PostRoomImagesAsync(
            Guid roomId, List<IFormFile> imagesForms)
        {
            return CreateEntityImageAsync(
                roomId, imagesForms, _roomService.AddImagesForRoomAsync);
        }

        private async Task<IActionResult> CreateEntityImageAsync(
            Guid roomId, 
            List<IFormFile> imagesForms, 
            Func<Guid, IEnumerable<Image>, Task> addImagesAsync)
        {
            try
            {
                var images = imagesForms.ToImages();
                await addImagesAsync(roomId, images);
            }
            catch (UnknownImageFormatException)
            {
                return BadRequest("Invalid image format.");
            }
            catch (ValidationException ex)
            {
                return BadRequest(ex.GetErrorsForClient());
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (EntityImagesLimitExceededException ex)
            {
                return BadRequest(
                    $"The limit of the allowed images per entity ({ex.ExceededLimit}) is exceeded");
            }

            return Created();
        }
    }
}
