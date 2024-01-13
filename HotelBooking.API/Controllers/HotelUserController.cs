﻿using FluentValidation;
using HotelBooking.Api.Extensions;
using HotelBooking.Domain.Abstractions.Services;
using HotelBooking.Domain.Constants;
using HotelBooking.Domain.Models;
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
        private readonly IHotelService _hotelService;

        public HotelUserController(IHotelService hotelService)
        {
            _hotelService = hotelService;
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

            try
            {
                hotel = await _hotelService.GetHotelPageAsync(hotelId);
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
            int reviewsCount = 0;

            try
            {
                reviews = await _hotelService.GetReviewsByPageAsync(hotelId, pagination);
                reviewsCount = await _hotelService.GetReviewsCountAsync(hotelId);
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
            int featuredHotelsCount = 0;

            try
            {
                featuredHotels = await _hotelService.GetFeaturedHotelsByPageAsync(pagination);
                featuredHotelsCount = _hotelService.GetFeaturedHotelsCount();
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
            int hotelsCount = 0;

            try
            {
                hotels = await _hotelService.SearchForUserByPageAsync(hotelSearch, pagination);
                hotelsCount = _hotelService.GetSearchForUserCount(hotelSearch);
            }
            catch (ValidationException ex)
            {
                return BadRequest(ex.GetErrorsForClient());
            }

            Response.Headers.AddPaginationMetadata(hotelsCount, pagination);

            return Ok(hotels);
        }
    }
}