using AutoMapper;
using FluentValidation;
using HotelBooking.Api.Extensions;
using HotelBooking.Api.Models;
using HotelBooking.Domain.Abstractions.Services;
using HotelBooking.Domain.Exceptions;
using HotelBooking.Domain.Models.User;
using Microsoft.AspNetCore.Mvc;

namespace HotelBooking.Api.Controllers
{
    [ApiController]
    [Route("api/auth")]
    public class AuthenticationController : Controller
    {
        private readonly IUserService _userService;
        private readonly IMapper _mapper;
        private readonly ILogger<AuthenticationController> _logger;

        public AuthenticationController(
            IUserService userService, IMapper mapper, ILogger<AuthenticationController> logger)
        {
            _userService = userService;
            _mapper = mapper;
            _logger = logger;
        }

        /// <summary>
        /// Create and store a new user.
        /// </summary>
        /// <param name="newUser">Properties of the new user.</param>
        [HttpPost("user-register")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public async Task<IActionResult> PostUserAsync(UserCreationDTO newUser)
        {
            try
            {
                await _userService.AddAsync(_mapper.Map<UserDTO>(newUser));
            }
            catch (ValidationException ex)
            {
                return BadRequest(ex.GetErrorsForClient());
            }

            return Created();
        }

        /// <summary>
        /// Login to an existing user account.
        /// </summary>
        /// <response code="200">User is authenticated. Authentication token is returned.</response>
        [HttpPost("user-login")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(string), StatusCodes.Status200OK)]
        public async Task<IActionResult> LoginAsync(UserLoginDTO userLogin)
        {
            string authenticationToken;

            try
            {
                authenticationToken = await _userService.AuthenticateAsync(userLogin);
            }
            catch (ValidationException ex)
            {
                return BadRequest(ex.GetErrorsForClient());
            }
            catch (InvalidUserCredentialsException)
            {
                return Unauthorized();
            }

            _logger.LogInformation("User with username: {username} logged in", userLogin.Username);
            return Ok(authenticationToken);
        }
    }
}
