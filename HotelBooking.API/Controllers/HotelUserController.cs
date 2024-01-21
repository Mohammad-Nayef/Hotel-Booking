using FluentValidation;
using HotelBooking.Api.Extensions;
using HotelBooking.Domain.Abstractions.Services.Hotel;
using HotelBooking.Domain.Constants;
using HotelBooking.Domain.Models;
using HotelBooking.Domain.Models.Hotel;
using HotelBooking.Domain.Models.Room;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HotelBooking.Api.Controllers
{
    [Authorize(Roles = $"{UserRoles.Admin}, {UserRoles.RegularUser}")]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    [ApiController]
    [Route("api/hotels")]
    public class HotelUserController : Controller
    {
        private readonly IHotelUserService _hotelUserService;

        public HotelUserController(IHotelUserService hotelUserService)
        {
            _hotelUserService = hotelUserService;
        }

        /// <summary>
        /// Get hotel info by Id.
        /// </summary>
        /// <param name="hotelId">Id of the hotel.</param>
        /// <response code="404">The hotel Id does not exists.</response>
        /// <response code="200">Returns the requested hotel info.</response>
        [HttpGet("{hotelId}")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(HotelPageDTO), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetHotel(Guid hotelId)
        {
            HotelPageDTO hotel;
            var userId = new Guid(HttpContext.User.Identity.Name);

            try
            {
                hotel = await _hotelUserService.GetHotelPageAsync(hotelId, userId);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }

            return Ok(hotel);
        }

        /// <summary>
        /// Get a paginated list of reviews for a specific hotel.
        /// </summary>
        /// <param name="hotelId">Id of the hotel to get its reviews.</param>
        /// <response code="404">The hotel Id does not exists.</response>
        /// <response code="200">Returns the requested hotel reviews.</response>
        [HttpGet("{hotelId}/reviews")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(IEnumerable<ReviewForHotelPageDTO>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetHotelReviewsAsync(
            Guid hotelId, [FromQuery] PaginationDTO pagination)
        {
            IEnumerable<ReviewForHotelPageDTO> reviews;
            int reviewsCount;

            try
            {
                reviews = await _hotelUserService.GetReviewsByPageAsync(hotelId, pagination);
                reviewsCount = await _hotelUserService.GetReviewsCountAsync(hotelId);
            }
            catch (ValidationException ex)
            {
                return BadRequest(ex.GetErrorsForClient());
            }
            catch (KeyNotFoundException)
            {
                return NotFound();
            }

            Response.Headers.AddPaginationMetadata(reviewsCount, pagination);

            return Ok(reviews);
        }

        /// <summary>
        /// Get a paginated list of featured hotels.
        /// </summary>
        /// <response code="200">Returns the list of featured hotels.</response>
        [HttpGet("featured")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(IEnumerable<FeaturedHotelDTO>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetFeaturedHotelsAsync(
            [FromQuery] PaginationDTO pagination)
        {
            IEnumerable<FeaturedHotelDTO> featuredHotels;
            int featuredHotelsCount;

            try
            {
                featuredHotels = await _hotelUserService.GetFeaturedHotelsByPageAsync(pagination);
                featuredHotelsCount = _hotelUserService.GetFeaturedHotelsCount();
            }
            catch (ValidationException ex)
            {
                return BadRequest(ex.GetErrorsForClient());
            }

            Response.Headers.AddPaginationMetadata(featuredHotelsCount, pagination);

            return Ok(featuredHotels);
        }

        /// <summary>
        /// Global search method for paginated list of hotels by user.
        /// </summary>
        /// <param name="hotelSearch">Search criteria properties.</param>
        /// <response code="200">Returns the list of relevant hotels.</response>
        [HttpPost("search")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(IEnumerable<HotelForUserDTO>), StatusCodes.Status200OK)]
        public async Task<IActionResult> HotelsSearchAsync(
            [FromQuery] PaginationDTO pagination, HotelSearchDTO hotelSearch)
        {
            IEnumerable<HotelForUserDTO> hotels;
            int hotelsCount;

            try
            {
                hotels = await _hotelUserService.SearchByPageAsync(hotelSearch, pagination);
                hotelsCount = _hotelUserService.GetSearchCount(hotelSearch);
            }
            catch (ValidationException ex)
            {
                return BadRequest(ex.GetErrorsForClient());
            }

            Response.Headers.AddPaginationMetadata(hotelsCount, pagination);

            return Ok(hotels);
        }

        /// <summary>
        /// Get paginated list of available rooms in an hotel.
        /// </summary>
        /// <param name="hotelId">Id of the hotel to get its available rooms.</param>
        /// <response code="200">Returns the list of available rooms.</response>
        [HttpGet("{hotelId}/rooms/available")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(IEnumerable<RoomForUserDTO>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAvailableRoomsAsync(
            Guid hotelId, [FromQuery] PaginationDTO pagination)
        {
            IEnumerable<RoomForUserDTO> rooms;
            int roomsCount;

            try
            {
                rooms = await _hotelUserService.GetAvailableRoomsAsync(hotelId, pagination);
                roomsCount = _hotelUserService.GetAvailableRoomsCount(hotelId);
            }
            catch (ValidationException ex)
            {
                return BadRequest(ex.GetErrorsForClient());
            }
            catch (KeyNotFoundException)
            {
                return NotFound();
            }

            Response.Headers.AddPaginationMetadata(roomsCount, pagination);

            return Ok(rooms);
        }

        /// <summary>
        /// Get paginated list of recently visited hotels.
        /// </summary>
        /// <response code="200">Returns the list of visited hotels.</response>
        [HttpGet("recently-visited/current-user")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(IEnumerable<VisitedHotelDTO>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetRecentlyVisitedHotelsAsync(
            [FromQuery] PaginationDTO pagination)
        {
            var userId = new Guid(HttpContext.User.Identity.Name);
            IEnumerable<VisitedHotelDTO> visitedHotels;
            int visitedHotelsCount = 0;

            try
            {
                visitedHotels = await _hotelUserService.GetRecentlyVisitedByPageAsync(
                    userId, pagination);
                visitedHotelsCount = await _hotelUserService.GetRecentlyVisitedCountAsync(userId);
            }
            catch (ValidationException ex)
            {
                return BadRequest(ex.GetErrorsForClient());
            }

            Response.Headers.AddPaginationMetadata(visitedHotelsCount, pagination);

            return Ok(visitedHotels);
        }
    }
}
