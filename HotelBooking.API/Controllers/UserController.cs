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

        public UserController(IMapper mapper, ICartItemService cartItemService)
        {
            _mapper = mapper;
            _cartItemService = cartItemService;
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
    }
}
