using FluentValidation;
using HotelBooking.Api.Extensions;
using HotelBooking.Domain.Abstractions.Services.City;
using HotelBooking.Domain.Constants;
using HotelBooking.Domain.Models;
using HotelBooking.Domain.Models.City;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HotelBooking.Api.Controllers
{
    [Authorize(Roles = $"{UserRoles.Admin}, {UserRoles.RegularUser}")]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    [ApiController]
    [Route("api/cities")]
    public class CityUserController : Controller
    {
        private readonly ICityService _cityService;

        public CityUserController(ICityService cityService)
        {
            _cityService = cityService;
        }

        /// <summary>
        /// Get a paginated list of popular cities.
        /// </summary>
        /// <response code="200">Returns the list of featured hotels.</response>
        [HttpGet("popular")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(IEnumerable<PopularCityDTO>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetPopularCitiesAsync(
            [FromQuery] PaginationDTO pagination)
        {
            IEnumerable<PopularCityDTO> popularCities;
            int popularCitiesCount;

            try
            {
                popularCities = await _cityService.GetPopularCitiesByPageAsync(pagination);
                popularCitiesCount = await _cityService.GetCountAsync();
            }
            catch (ValidationException ex)
            {
                return BadRequest(ex.GetErrorsForClient());
            }

            Response.Headers.AddPaginationMetadata(popularCitiesCount, pagination);

            return Ok(popularCities);
        }
    }
}
