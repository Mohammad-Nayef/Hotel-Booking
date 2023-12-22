using AutoMapper;
using FluentValidation;
using HotelBooking.Api.Extensions;
using HotelBooking.Api.Models;
using HotelBooking.Domain.Abstractions.Services;
using HotelBooking.Domain.Models;
using Microsoft.AspNetCore.Mvc;

namespace HotelBooking.Api.Controllers
{
    [ApiController]
    [Route("api/rooms")]
    public class RoomController : Controller
    {
        private readonly IRoomService _roomService;
        private readonly IMapper _mapper;

        public RoomController(IRoomService roomService, IMapper mapper)
        {
            _roomService = roomService;
            _mapper = mapper;
        }

        /// <summary>
        /// Get a paginated list of rooms for an admin.
        /// </summary>
        /// <response code="200">The list of rooms is retrieved successfully.</response>
        [HttpGet("admin-view")]
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
        /// Create and store a new room.
        /// </summary>
        /// <param name="newRoom">Properties of the new room.</param>
        /// <response code="201">Returns the room with a new Id and its URI in response headers.</response>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(RoomCreationDTO), StatusCodes.Status201Created)]
        public async Task<IActionResult> PostAsync(RoomCreationDTO newRoom)
        {
            Guid newId;

            try
            {
                newId = await _roomService.AddAsync(_mapper.Map<RoomDTO>(newRoom));
            }
            catch (ValidationException ex)
            {
                return BadRequest(ex.GetErrorsForClient());
            }

            var createdRoom = _mapper.Map<RoomForCreationResponseDTO>(newRoom);
            createdRoom.Id = newId;

            return Created($"api/rooms/{newId}", createdRoom);
        }

        /// <summary>
        /// Delete a room with a specific Id.
        /// </summary>
        /// <param name="id">The Id of the room to delete.</param>
        /// <response code="404">The room with the given Id doesn't exist.</response>
        /// <response code="204">The room is deleted successfully.</response>
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> DeleteAsync(Guid id)
        {
            try
            {
                await _roomService.DeleteAsync(id);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }

            return NoContent();
        }

    }
}