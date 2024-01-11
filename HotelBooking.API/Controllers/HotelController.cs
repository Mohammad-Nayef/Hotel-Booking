using AutoMapper;
using FluentValidation;
using HotelBooking.Api.Extensions;
using HotelBooking.Api.Models;
using HotelBooking.Domain.Abstractions.Services;
using HotelBooking.Domain.Constants;
using HotelBooking.Domain.Exceptions;
using HotelBooking.Domain.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using SixLabors.ImageSharp;

namespace HotelBooking.Api.Controllers
{
    [Authorize(Roles = $"{UserRoles.Admin}")]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    [ApiController]
    [Route("api/hotels")]
    public class HotelController : Controller
    {
        private readonly IHotelService _hotelService;
        private readonly IMapper _mapper;
        private readonly IDiscountService _discountService;

        public HotelController(
            IHotelService hotelService, IMapper mapper, IDiscountService discountService)
        {
            _hotelService = hotelService;
            _mapper = mapper;
            _discountService = discountService;
        }

        /// <summary>
        /// Create and store a new hotel.
        /// </summary>
        /// <param name="newHotel">Properties of the new hotel.</param>
        /// <response code="201">Returns the hotel with a new Id and its URI in response headers.</response>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(HotelCreationDTO), StatusCodes.Status201Created)]
        public async Task<IActionResult> PostAsync(HotelCreationDTO newHotel)
        {
            Guid newId;

            try
            {
                newId = await _hotelService.AddAsync(_mapper.Map<HotelDTO>(newHotel));
            }
            catch (ValidationException ex)
            {
                return BadRequest(ex.GetErrorsForClient());
            }

            var createdHotel = _mapper.Map<HotelCreationResponseDTO>(newHotel);
            createdHotel.Id = newId;

            return Created($"api/hotels/{newId}", createdHotel);
        }

        /// <summary>
        /// Add images for a hotel.
        /// </summary>
        /// <param name="hotelId">Id of the hotel to add images for.</param>        
        /// <response code="404">The hotel with the given Id doesn't exist.</response>
        /// <response code="204">The images are successfully added.</response>
        [HttpPost("{hotelId}/images")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public async Task<IActionResult> PostHotelImagesAsync(Guid hotelId, List<IFormFile> imagesForms)
        {
            try
            {
                var images = imagesForms.ToImages();
                await _hotelService.AddImagesForHotelAsync(hotelId, images);
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

        /// <summary>
        /// Delete a hotel with a specific Id.
        /// </summary>
        /// <param name="hotelId">The Id of the hotel to delete.</param>
        /// <response code="404">The hotel with the given Id doesn't exist.</response>
        /// <response code="204">The hotel is deleted successfully.</response>
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> DeleteAsync(Guid hotelId)
        {
            try
            {
                await _hotelService.DeleteAsync(hotelId);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }

            return NoContent();
        }

        /// <summary>
        /// Partially update a specific hotel.
        /// </summary>
        /// <param name="hotelId">The Id of the hotel to update.</param>
        /// <param name="hotelPatchDocument">Patch operations for (Name, OwnerName, Geolocation, CityId)</param>
        /// <response code="404">The hotel with the given Id doesn't exist.</response>
        /// <response code="204">The hotel is updated successfully.</response>
        [HttpPatch("{hotelId}")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> PatchAsync(
            Guid hotelId, JsonPatchDocument<HotelUpdateDTO> hotelPatchDocument)
        {
            HotelDTO hotelToUpdate;

            try
            {
                hotelToUpdate = await _hotelService.GetByIdAsync(hotelId);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }

            var hotelToPartiallyUpdate = GetHotelToPartiallyUpdate(
                hotelPatchDocument, hotelToUpdate);

            if (!ModelState.IsValid || !TryValidateModel(hotelToPartiallyUpdate))
                return BadRequest(ModelState);

            _mapper.Map(hotelToPartiallyUpdate, hotelToUpdate);

            try
            {
                await _hotelService.UpdateAsync(hotelToUpdate);
            }
            catch (ValidationException ex)
            {
                return BadRequest(ex.GetErrorsForClient());
            }

            return NoContent();
        }

        private HotelUpdateDTO GetHotelToPartiallyUpdate(
            JsonPatchDocument<HotelUpdateDTO> hotelPatchDocument, HotelDTO hotelToUpdate)
        {
            var hotelToPartiallyUpdate = _mapper.Map<HotelUpdateDTO>(hotelToUpdate);
            hotelPatchDocument.ApplyTo(hotelToPartiallyUpdate, ModelState);

            return hotelToPartiallyUpdate;
        }

        /// <summary>
        /// Create and store a new discount.
        /// </summary>
        /// <param name="hotelId">The Id of the hotel that has the discount.</param>
        /// <param name="newDiscount">Properties of the new discount.</param>
        /// <response code="201">Returns the discount with a new Id and its URI in response headers.</response>
        [HttpPost("{hotelId}/discounts")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(DiscountCreationDTO), StatusCodes.Status201Created)]
        public async Task<IActionResult> PostDiscountAsync(
            Guid hotelId, DiscountCreationDTO newDiscount)
        {
            Guid newId;
            newDiscount.HotelId = hotelId;

            try
            {
                newId = await _discountService.AddAsync(_mapper.Map<DiscountDTO>(newDiscount));
            }
            catch (ValidationException ex)
            {
                return BadRequest(ex.GetErrorsForClient());
            }

            var createdDiscount = _mapper.Map<DiscountCreationResponseDTO>(newDiscount);
            createdDiscount.Id = newId;

            return Created(
                $"api/hotels/{createdDiscount.HotelId}/discounts/{newId}", createdDiscount);
        }
    }
}
