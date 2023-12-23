using FluentValidation;
using HotelBooking.Api.Extensions;
using HotelBooking.Domain.Abstractions.Services;
using HotelBooking.Domain.Models;
using Microsoft.AspNetCore.Mvc;

namespace HotelBooking.Api.Controllers
{
    [ApiController]
    [Route("admins")]
    public class AdminController : Controller
    {
        private readonly ICityService _cityService;
        private readonly IHotelService _hotelService;
        private readonly IRoomService _roomService;

        public AdminController(
            ICityService cityService, IHotelService hotelService, IRoomService roomService)
        {
            _cityService = cityService;
            _hotelService = hotelService;
            _roomService = roomService;
        }

        /// <summary>
        /// Get a paginated list of cities for an admin.
        /// </summary>
        /// <response code="200">The list of cities is retrieved successfully.</response>
        [HttpGet("cities")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(IEnumerable<CityForAdminDTO>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetCitiesForAdminAsync(
            [FromQuery] PaginationDTO pagination)
        {
            IEnumerable<CityForAdminDTO> cities;

            try
            {
                cities = await _cityService.GetForAdminByPageAsync(pagination);
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
        /// Get a paginated list of hotels for an admin.
        /// </summary>
        /// <response code="200">The list of hotels is retrieved successfully.</response>
        [HttpGet("hotels")]
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
        /// Get a paginated list of rooms for an admin.
        /// </summary>
        /// <response code="200">The list of rooms is retrieved successfully.</response>
        [HttpGet("rooms")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(IEnumerable<RoomForAdminDTO>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetRoomsForAdminAsync(
            [FromQuery] PaginationDTO pagination)
        {
            IEnumerable<RoomForAdminDTO> rooms;

            try
            {
                rooms = await _roomService.GetForAdminByPageAsync(pagination);
            }
            catch (ValidationException ex)
            {
                return BadRequest(ex.GetErrorsForClient());
            }

            var roomsCount = await _roomService.GetCountAsync();
            Response.Headers.AddPaginationMetadata(roomsCount, pagination);

            return Ok(rooms);
        }

        /// <summary>
        /// Get Paginated list of cities for an admin based on search query.
        /// </summary>
        /// <param name="search">The search query</param>
        /// <response code="200">The list of cities is retrieved successfully.</response>
        [HttpPost("cities/search")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(IEnumerable<CityForAdminDTO>), StatusCodes.Status200OK)]
        public async Task<IActionResult> SearchCitiesForAdminAsync(
            [FromQuery] PaginationDTO pagination, [FromBody] string search)
        {
            IEnumerable<CityForAdminDTO> cities;

            try
            {
                cities = await _cityService.SearchByCityForAdminByPageAsync(pagination, search);
            }
            catch (ValidationException ex)
            {
                return BadRequest(ex.GetErrorsForClient());
            }

            var citiesCount = await _cityService.GetSearchByCityForAdminCountAsync(search);
            Response.Headers.AddPaginationMetadata(citiesCount, pagination);

            return Ok(cities);
        }

        /// <summary>
        /// Get Paginated list of hotels for an admin based on search query.
        /// </summary>
        /// <param name="search">The search query</param>
        /// <response code="200">The list of hotels is retrieved successfully.</response>
        [HttpPost("hotels/search")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(IEnumerable<HotelForAdminDTO>), StatusCodes.Status200OK)]
        public async Task<IActionResult> SearchHotelsForAdminAsync(
            [FromQuery] PaginationDTO pagination, [FromBody] string search)
        {
            IEnumerable<HotelForAdminDTO> hotels;

            try
            {
                hotels = await _hotelService.SearchByHotelForAdminByPageAsync(pagination, search);
            }
            catch (ValidationException ex)
            {
                return BadRequest(ex.GetErrorsForClient());
            }

            var hotelsCount = await _hotelService.GetSearchByHotelForAdminCountAsync(search);
            Response.Headers.AddPaginationMetadata(hotelsCount, pagination);

            return Ok(hotels);
        }
    }
}
