using AutoMapper;
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
    [Authorize(Roles = $"{UserRoles.Admin}, {UserRoles.RegularUser}")]
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

        public UserController(
            IMapper mapper,
            ICartItemService cartItemService,
            IBookingService bookingService,
            IHotelReviewService hotelReviewService)
        {
            _mapper = mapper;
            _cartItemService = cartItemService;
            _bookingService = bookingService;
            _hotelReviewService = hotelReviewService;
        }

        /// <summary>
        /// Create and store a new cart item for a user.
        /// </summary>
        /// <param name="newCartItem">Properties of the new cart item.</param>
        [HttpPost("current-user/cart-items")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public async Task<IActionResult> PostCartItemAsync(CartItemCreationDTO newCartItem)
        {
            var userId = new Guid(HttpContext.User.Identity.Name);
            newCartItem.UserId = userId;

            try
            {
                await _cartItemService.AddAsync(_mapper.Map<CartItemDTO>(newCartItem));
            }
            catch (ValidationException ex)
            {
                return BadRequest(ex.GetErrorsForClient());
            }

            return Created();
        }

        /// <summary>
        /// Get a paginated list of cart items for a user.
        /// </summary>
        /// <response code="200">The list of cart items is retrieved successfully.</response>
        [HttpGet("current-user/cart-items")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(IEnumerable<CartItemDTO>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetCartItemsAsync([FromQuery] PaginationDTO pagination)
        {
            var userId = new Guid(HttpContext.User.Identity.Name);
            IEnumerable<CartItemDTO> cartItems;
            int citiesCount = 0;

            try
            {
                cartItems = await _cartItemService.GetAllForUserByPageAsync(userId, pagination);
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
        /// Create, store a new booking for a user and send an email of its details to the user.
        /// </summary>
        /// <param name="newBooking">Properties of the new booking.</param>
        [HttpPost("current-user/bookings")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public async Task<IActionResult> PostBookingAsync(BookingCreationDTO newBooking)
        {
            newBooking.UserId = new Guid(HttpContext.User.Identity.Name);

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
        /// <param name="newReview">Properties of the new review.</param>
        /// <response code="201">The review is created successfully.</response>
        [HttpPost("current-user/hotel-reviews")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public async Task<IActionResult> PostHotelReviewAsync(HotelReviewCreationDTO newReview)
        {
            newReview.UserId = new Guid(HttpContext.User.Identity.Name);

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
    }
}
