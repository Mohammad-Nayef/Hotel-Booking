using AutoMapper;
using FluentValidation;
using HotelBooking.Api.Extensions;
using HotelBooking.Api.Models;
using HotelBooking.Api.Models.Hotel;
using HotelBooking.Domain.Abstractions.Services;
using HotelBooking.Domain.Abstractions.Services.Hotel;
using HotelBooking.Domain.Constants;
using HotelBooking.Domain.Models;
using HotelBooking.Domain.Models.Hotel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;

namespace HotelBooking.Api.Controllers
{
    [Authorize(Roles = $"{UserRoles.Admin}")]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    [ApiController]
    [Route("api/admin/hotels")]
    public class HotelAdminController : Controller
    {
        private readonly IHotelService _hotelService;
        private readonly IHotelAdminService _hotelAdminService;
        private readonly IMapper _mapper;
        private readonly IDiscountService _discountService;

        public HotelAdminController(
            IHotelService hotelService,
            IHotelAdminService hotelAdminService,
            IMapper mapper,
            IDiscountService discountService)
        {
            _hotelService = hotelService;
            _hotelAdminService = hotelAdminService;
            _mapper = mapper;
            _discountService = discountService;
        }

        /// <summary>
        /// Get a paginated list of hotels for an admin.
        /// </summary>
        /// <response code="200">The list of hotels is retrieved successfully.</response>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(IEnumerable<HotelForAdminDTO>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetHotelsForAdminAsync(
            [FromQuery] PaginationDTO pagination)
        {
            IEnumerable<HotelForAdminDTO> hotels;

            try
            {
                hotels = await _hotelAdminService.GetByPageAsync(pagination);
            }
            catch (ValidationException ex)
            {
                return BadRequest(ex.GetErrorsForClient());
            }

            var hotelsCount = await _hotelService.GetCountAsync();
            Response.Headers.AddPaginationMetadata(hotelsCount, pagination);

            return Ok(hotels);
        }

        /// <summary>
        /// Get paginated list of hotels for an admin based on search query.
        /// </summary>
        /// <param name="query">The search query</param>
        /// <response code="200">The list of hotels is retrieved successfully.</response>
        [HttpGet("search")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(IEnumerable<HotelForAdminDTO>), StatusCodes.Status200OK)]
        public async Task<IActionResult> SearchHotelsForAdminAsync(
            [FromQuery] PaginationDTO pagination, string query)
        {
            IEnumerable<HotelForAdminDTO> hotels;

            try
            {
                hotels = await _hotelAdminService.SearchByPageAsync(pagination, query);
            }
            catch (ValidationException ex)
            {
                return BadRequest(ex.GetErrorsForClient());
            }

            var hotelsCount = await _hotelAdminService.GetSearchCountAsync(query);
            Response.Headers.AddPaginationMetadata(hotelsCount, pagination);

            return Ok(hotels);
        }

        /// <summary>
        /// Create and store a new hotel.
        /// </summary>
        /// <param name="newHotel">Properties of the new hotel.</param>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public async Task<IActionResult> PostAsync(HotelCreationDTO newHotel)
        {
            try
            {
                await _hotelService.AddAsync(_mapper.Map<HotelDTO>(newHotel));
            }
            catch (ValidationException ex)
            {
                return BadRequest(ex.GetErrorsForClient());
            }

            return Created();
        }

        /// <summary>
        /// Delete a hotel with a specific Id.
        /// </summary>
        /// <param name="hotelId">The Id of the hotel to delete.</param>
        /// <response code="404">The hotel with the given Id doesn't exist.</response>
        /// <response code="204">The hotel is deleted successfully.</response>
        [HttpDelete("{hotelId}")]
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
        [HttpPost("{hotelId}/discounts")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public async Task<IActionResult> PostDiscountAsync(
            Guid hotelId, DiscountCreationDTO newDiscount)
        {
            newDiscount.HotelId = hotelId;

            try
            {
                await _discountService.AddAsync(_mapper.Map<DiscountDTO>(newDiscount));
            }
            catch (ValidationException ex)
            {
                return BadRequest(ex.GetErrorsForClient());
            }

            return Created();
        }
    }
}
