﻿using AutoMapper;
using FluentValidation;
using HotelBooking.Api.Extensions;
using HotelBooking.Api.Models;
using HotelBooking.Domain.Abstractions.Services;
using HotelBooking.Domain.Constants;
using HotelBooking.Domain.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HotelBooking.Api.Controllers
{
    [Authorize(Roles = $"{UserRoles.Admin}, {UserRoles.Regular}")]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    [ApiController]
    [Route("api/users")]
    public class UserController : Controller
    {
        private readonly IMapper _mapper;
        private readonly ICartItemService _cartItemService;
        private readonly IBookingService _bookingService;
        private readonly IHotelReviewService _hotelReviewService;
        private readonly IHotelService _hotelService;

        public UserController(
            IMapper mapper,
            ICartItemService cartItemService,
            IBookingService bookingService,
            IHotelReviewService hotelReviewService,
            IHotelService hotelService)
        {
            _mapper = mapper;
            _cartItemService = cartItemService;
            _bookingService = bookingService;
            _hotelReviewService = hotelReviewService;
            _hotelService = hotelService;
        }

        /// <summary>
        /// Create and store a new cart item for a user.
        /// </summary>
        /// <param name="userId">The Id of the user that has the cart item.</param>
        /// <param name="newCartItem">Properties of the new cart item.</param>
        /// <response code="201">Returns the cart item with a new Id and its URI in response headers.</response>
        [HttpPost("{userId}/cart-items")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(CartItemCreationDTO), StatusCodes.Status201Created)]
        public async Task<IActionResult> PostCartItemAsync(
            Guid userId, CartItemCreationDTO newCartItem)
        {
            Guid newId;
            newCartItem.UserId = userId;

            try
            {
                newId = await _cartItemService.AddAsync(_mapper.Map<CartItemDTO>(newCartItem));
            }
            catch (ValidationException ex)
            {
                return BadRequest(ex.GetErrorsForClient());
            }

            var createdCartItem = _mapper.Map<CartItemCreationResponseDTO>(newCartItem);
            createdCartItem.Id = newId;

            return Created(
                $"api/users/{createdCartItem.UserId}/cart-items/{newId}", createdCartItem);
        }

        /// <summary>
        /// Get a paginated list of cart items for a user.
        /// </summary>
        /// <param name="userId">The Id of the user to get his cart items.</param>
        /// <response code="200">The list of cart items is retrieved successfully.</response>
        [HttpGet("{userId}/cart-items")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(IEnumerable<CartItemDTO>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetCartItemsAsync(
            Guid userId, [FromQuery] PaginationDTO pagination)
        {
            IEnumerable<CartItemDTO> cartItems;
            int citiesCount = 0;

            try
            {
                cartItems = await _cartItemService.GetAllForUserByPage(userId, pagination);
                citiesCount = await _cartItemService.GetCountForUserAsync(userId);
            }
            catch (ValidationException ex)
            {
                return BadRequest(ex.GetErrorsForClient());
            }
            catch (KeyNotFoundException)
            {
                return BadRequest("Invalid user id.");
            }

            Response.Headers.AddPaginationMetadata(citiesCount, pagination);

            return Ok(cartItems);
        }

        /// <summary>
        /// Create and store a new booking for a user.
        /// </summary>
        /// <param name="userId">The Id of the user that has the booking.</param>
        /// <param name="newBooking">Properties of the new booking.</param>
        /// <response code="201">The booking is created successfully.</response>
        [HttpPost("{userId}/bookings")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public async Task<IActionResult> PostBookingAsync(
            Guid userId, BookingCreationDTO newBooking)
        {
            newBooking.UserId = userId;

            try
            {
                await _bookingService.AddAsync(_mapper.Map<BookingDTO>(newBooking));
            }
            catch (ValidationException ex)
            {
                return BadRequest(ex.GetErrorsForClient());
            }

            return Created();
        }

        /// <summary>
        /// Create and store a new hotel review for a user.
        /// </summary>
        /// <param name="userId">The Id of the user creating the review.</param>
        /// <param name="newReview">Properties of the new review.</param>
        /// <response code="201">The review is created successfully.</response>
        [HttpPost("{userId}/hotel-reviews")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public async Task<IActionResult> PostHotelReviewAsync(
            Guid userId, HotelReviewCreationDTO newReview)
        {
            newReview.UserId = userId;

            try
            {
                await _hotelReviewService.AddAsync(_mapper.Map<HotelReviewDTO>(newReview));
            }
            catch (ValidationException ex)
            {
                return BadRequest(ex.GetErrorsForClient());
            }

            return Created();
        }

        /// <summary>
        /// Get a paginated list of featured hotels.
        /// </summary>
        /// <response code="200">Returns the list of featured hotels.</response>
        [HttpGet("hotels/featured")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetFeaturedHotelsAsync(
            [FromQuery] PaginationDTO pagination)
        {
            IEnumerable<FeaturedHotelDTO> featuredHotels;

            try
            {
                featuredHotels = await _hotelService.GetFeaturedHotelsByPageAsync(pagination);
            }
            catch (ValidationException ex)
            {
                return BadRequest(ex.GetErrorsForClient());
            }

            return Ok(featuredHotels);
        }
    }
}
