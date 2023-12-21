﻿using AutoMapper;
using FluentValidation;
using HotelBooking.Api.Extensions;
using HotelBooking.Api.Models;
using HotelBooking.Domain.Abstractions.Services;
using HotelBooking.Domain.Models;
using Microsoft.AspNetCore.Mvc;

namespace HotelBooking.Api.Controllers
{
    [ApiController]
    [Route("api/cities")]
    public class CityController : Controller
    {
        private readonly ICityService _cityService;
        private readonly IMapper _mapper;

        public CityController(ICityService cityService, IMapper mapper)
        {
            _cityService = cityService;
            _mapper = mapper;
        }

        /// <summary>
        /// Create and store a new city.
        /// </summary>
        /// <param name="newCity">Properties of the new city.</param>
        /// <response code="201">Returns the city with a new Id and its URI in response headers.</response>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(CityCreationDTO), StatusCodes.Status201Created)]
        public async Task<IActionResult> PostCityAsync(CityCreationDTO newCity)
        {
            Guid newId;

            try
            {
                newId = await _cityService.AddAsync(_mapper.Map<CityDTO>(newCity));
            }
            catch (ValidationException ex)
            {
                return BadRequest(ex.GetErrorsForClient());
            }

            var createdCity = _mapper.Map<CityForCreationResponseDTO>(newCity);
            createdCity.Id = newId;

            return Created($"api/cities/{newId}", createdCity);
        }

        /// <summary>
        /// Delete a city with a specific Id.
        /// </summary>
        /// <param name="id">The Id of the city to delete.</param>
        /// <response code="404">The city with the given Id doesn't exist.</response>
        /// <response code="204">The city is deleted successfully.</response>
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> DeleteAsync(Guid id)
        {
            try
            {
                await _cityService.DeleteAsync(id);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }

            return NoContent();
        }
    }
}
