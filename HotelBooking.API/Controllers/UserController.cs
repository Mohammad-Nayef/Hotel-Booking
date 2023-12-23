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
    [Route("api/users")]
    public class UserController : Controller
    {
        private readonly IUserService _userService;
        private readonly IMapper _mapper;

        public UserController(IUserService userService, IMapper mapper)
        {
            _userService = userService;
            _mapper = mapper;
        }

        /// <summary>
        /// Create and store a new user.
        /// </summary>
        /// <param name="newUser">Properties of the new user.</param>
        /// <response code="201">Returns the user with a new Id and its URI in response headers.</response>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(UserCreationDTO), StatusCodes.Status201Created)]
        public async Task<IActionResult> PostUserAsync(UserCreationDTO newUser)
        {
            Guid newId;

            try
            {
                newId = await _userService.AddAsync(_mapper.Map<UserDTO>(newUser));
            }
            catch (ValidationException ex)
            {
                return BadRequest(ex.GetErrorsForClient());
            }

            var createdUser = _mapper.Map<UserCreationResponseDTO>(newUser);
            createdUser.Id = newId;

            return Created($"api/users/{newId}", createdUser);
        }
    }
}