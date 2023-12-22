﻿using AutoMapper;
using FluentValidation;
using HotelBooking.Api.Extensions;
using HotelBooking.Api.Models;
using HotelBooking.Domain.Abstractions.Services;
using HotelBooking.Domain.Models;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;

namespace HotelBooking.Api.Controllers
{
    [ApiController]
    [Route("api/hotels")]
    public class HotelController : Controller
    {
        private readonly IHotelService _hotelService;
        private readonly IMapper _mapper;

        public HotelController(IHotelService hotelService, IMapper mapper)
        {
            _hotelService = hotelService;
            _mapper = mapper;
        }

        /// <summary>
        /// Get a paginated list of hotels for an admin.
        /// </summary>
        /// <response code="200">The list of hotels is retrieved successfully.</response>
        [HttpGet("admin-view")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(IEnumerable<HotelForAdminDTO>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetHotelsForAdminAsync(
            [FromQuery] PaginationDTO pagination)
        {
            IEnumerable<HotelForAdminDTO> hotels;

            try
            {
                hotels = await _hotelService.GetForAdminByPageAsync(pagination);
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
    }
}