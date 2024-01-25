using AutoMapper;
using FluentValidation;
using HotelBooking.Api.Extensions;
using HotelBooking.Api.Models.City;
using HotelBooking.Domain.Abstractions.Services.City;
using HotelBooking.Domain.Constants;
using HotelBooking.Domain.Models;
using HotelBooking.Domain.Models.City;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;

namespace HotelBooking.Api.Controllers
{
    [Authorize(Roles = $"{UserRoles.Admin}")]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    [ApiController]
    [Route("api/admin/cities")]
    public class CityAdminController : Controller
    {
        private readonly ICityService _cityService;
        private readonly IMapper _mapper;
        private readonly ICityAdminService _cityAdminService;

        public CityAdminController(
            ICityService cityService, IMapper mapper, ICityAdminService cityAdminService)
        {
            _cityService = cityService;
            _mapper = mapper;
            _cityAdminService = cityAdminService;
        }

        /// <summary>
        /// Get a paginated list of cities for an admin.
        /// </summary>
        /// <response code="200">The list of cities is retrieved successfully.</response>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(IEnumerable<CityForAdminDTO>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetCitiesForAdminAsync(
            [FromQuery] PaginationDTO pagination)
        {
            IEnumerable<CityForAdminDTO> cities;

            try
            {
                cities = await _cityAdminService.GetByPageAsync(pagination);
            }
            catch (ValidationException ex)
            {
                return BadRequest(ex.GetErrorsForClient());
            }

            var citiesCount = await _cityService.GetCountAsync();
            Response.Headers.AddPaginationMetadata(citiesCount, pagination);

            return Ok(cities);
        }

        /// <summary>
        /// Get paginated list of cities for an admin based on search query.
        /// </summary>
        /// <param name="query">The search query</param>
        /// <response code="200">The list of cities is retrieved successfully.</response>
        [HttpGet("search")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(IEnumerable<CityForAdminDTO>), StatusCodes.Status200OK)]
        public async Task<IActionResult> SearchCitiesForAdminAsync(
            [FromQuery] PaginationDTO pagination, string query)
        {
            IEnumerable<CityForAdminDTO> cities;

            try
            {
                cities = await _cityAdminService.SearchByPageAsync(pagination, query);
            }
            catch (ValidationException ex)
            {
                return BadRequest(ex.GetErrorsForClient());
            }

            var citiesCount = await _cityAdminService.GetSearchCountAsync(query);
            Response.Headers.AddPaginationMetadata(citiesCount, pagination);

            return Ok(cities);
        }

        /// <summary>
        /// Create and store a new city.
        /// </summary>
        /// <param name="newCity">Properties of the new city.</param>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public async Task<IActionResult> PostCityAsync(CityCreationDTO newCity)
        {
            try
            {
                await _cityService.AddAsync(_mapper.Map<CityDTO>(newCity));
            }
            catch (ValidationException ex)
            {
                return BadRequest(ex.GetErrorsForClient());
            }

            return Created();
        }

        /// <summary>
        /// Delete a city with a specific Id.
        /// </summary>
        /// <param name="cityId">The Id of the city to delete.</param>
        /// <response code="404">The city with the given Id doesn't exist.</response>
        /// <response code="204">The city is deleted successfully.</response>
        [HttpDelete("{cityId}")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> DeleteAsync(Guid cityId)
        {
            try
            {
                await _cityService.DeleteAsync(cityId);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }

            return NoContent();
        }

        /// <summary>
        /// Partially update a specific city.
        /// </summary>
        /// <param name="cityId">The Id of the city to update.</param>
        /// <param name="cityPatchDocument">Patch operations for (Name, CountryName, PostOffice).</param>
        /// <response code="404">The city with the given Id doesn't exist.</response>
        /// <response code="204">The city is updated successfully.</response>
        [HttpPatch("{cityId}")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> PatchCityAsync(
            Guid cityId, JsonPatchDocument<CityUpdateDTO> cityPatchDocument)
        {
            CityDTO cityToUpdate;

            try
            {
                cityToUpdate = await _cityService.GetByIdAsync(cityId);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }

            var cityToPartiallyUpdate = GetCityToPartiallyUpdate(
                cityPatchDocument, cityToUpdate);

            if (!ModelState.IsValid || !TryValidateModel(cityToPartiallyUpdate))
                return BadRequest(ModelState);

            _mapper.Map(cityToPartiallyUpdate, cityToUpdate);

            try
            {
                await _cityService.UpdateAsync(cityToUpdate);
            }
            catch (ValidationException ex)
            {
                return BadRequest(ex.GetErrorsForClient());
            }

            return NoContent();
        }

        private CityUpdateDTO GetCityToPartiallyUpdate(
            JsonPatchDocument<CityUpdateDTO> cityPatchDocument, CityDTO cityToUpdate)
        {
            var cityToPartiallyUpdate = _mapper.Map<CityUpdateDTO>(cityToUpdate);
            cityPatchDocument.ApplyTo(cityToPartiallyUpdate, ModelState);

            return cityToPartiallyUpdate;
        }
    }
}
