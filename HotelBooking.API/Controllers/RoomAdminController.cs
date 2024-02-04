using AutoMapper;
using FluentValidation;
using HotelBooking.Api.Extensions;
using HotelBooking.Api.Models.Room;
using HotelBooking.Domain.Abstractions.Services.Room;
using HotelBooking.Domain.Constants;
using HotelBooking.Domain.Models;
using HotelBooking.Domain.Models.Room;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;

namespace HotelBooking.Api.Controllers
{
    [Authorize(Roles = $"{UserRoles.Admin}")]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    [ApiController]
    [Route("api/admin/rooms")]
    public class RoomAdminController : Controller
    {
        private readonly IRoomService _roomService;
        private readonly IMapper _mapper;
        private readonly IRoomAdminService _roomAdminService;

        public RoomAdminController(
            IRoomService roomService, IMapper mapper, IRoomAdminService roomAdminService)
        {
            _roomService = roomService;
            _mapper = mapper;
            _roomAdminService = roomAdminService;
        }

        /// <summary>
        /// Get a paginated list of rooms for an admin.
        /// </summary>
        /// <response code="200">The list of rooms is retrieved successfully.</response>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(IEnumerable<RoomForAdminDTO>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetRoomsForAdminAsync(
            [FromQuery] PaginationDTO pagination)
        {
            IEnumerable<RoomForAdminDTO> rooms;

            try
            {
                rooms = await _roomAdminService.GetByPageAsync(pagination);
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
        /// Get Paginated list of rooms for an admin based on search query.
        /// </summary>
        /// <param name="pagination">Pagination parameters</param>
        /// <param name="query">The search query</param>
        /// <response code="200">The list of rooms is retrieved successfully.</response>
        [HttpGet("search")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(IEnumerable<RoomForAdminDTO>), StatusCodes.Status200OK)]
        public async Task<IActionResult> SearchRoomsForAdminAsync(
            [FromQuery] PaginationDTO pagination, string query)
        {
            IEnumerable<RoomForAdminDTO> rooms;

            try
            {
                rooms = await _roomAdminService.SearchByPageAsync(pagination, query);
            }
            catch (ValidationException ex)
            {
                return BadRequest(ex.GetErrorsForClient());
            }

            var roomsCount = await _roomAdminService.GetSearchCountAsync(query);
            Response.Headers.AddPaginationMetadata(roomsCount, pagination);

            return Ok(rooms);
        }

        /// <summary>
        /// Create and store a new room.
        /// </summary>
        /// <param name="newRoom">Properties of the new room.</param>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public async Task<IActionResult> PostAsync(RoomCreationDTO newRoom)
        {
            try
            {
                await _roomService.AddAsync(_mapper.Map<RoomDTO>(newRoom));
            }
            catch (ValidationException ex)
            {
                return BadRequest(ex.GetErrorsForClient());
            }

            return Created();
        }

        /// <summary>
        /// Delete a room with a specific Id.
        /// </summary>
        /// <param name="roomId">The Id of the room to delete.</param>
        /// <response code="404">The room with the given Id doesn't exist.</response>
        /// <response code="204">The room is deleted successfully.</response>
        [HttpDelete("{roomId}")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> DeleteAsync(Guid roomId)
        {
            try
            {
                await _roomService.DeleteAsync(roomId);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }

            return NoContent();
        }

        /// <summary>
        /// Partially update a specific room.
        /// </summary>
        /// <param name="roomId">The Id of the room to update.</param>
        /// <param name="roomPatchDocument">Patch operations for (Number, AdultsCapacity, ChildrenCapacity).</param>
        /// <response code="404">The room with the given Id doesn't exist.</response>
        /// <response code="204">The room is updated successfully.</response>
        [HttpPatch("{roomId}")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> PatchRoomAsync(
            Guid roomId, JsonPatchDocument<RoomUpdateDTO> roomPatchDocument)
        {
            RoomDTO roomToUpdate;

            try
            {
                roomToUpdate = await _roomService.GetByIdAsync(roomId);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }

            var roomToPartiallyUpdate = GetRoomToPartiallyUpdate(
                roomPatchDocument, roomToUpdate);

            if (!ModelState.IsValid || !TryValidateModel(roomToPartiallyUpdate))
                return BadRequest(ModelState);

            _mapper.Map(roomToPartiallyUpdate, roomToUpdate);

            try
            {
                await _roomService.UpdateAsync(roomToUpdate);
            }
            catch (ValidationException ex)
            {
                return BadRequest(ex.GetErrorsForClient());
            }

            return NoContent();
        }

        private RoomUpdateDTO GetRoomToPartiallyUpdate(
            JsonPatchDocument<RoomUpdateDTO> roomPatchDocument, RoomDTO roomToUpdate)
        {
            var roomToPartiallyUpdate = _mapper.Map<RoomUpdateDTO>(roomToUpdate);
            roomPatchDocument.ApplyTo(roomToPartiallyUpdate, ModelState);

            return roomToPartiallyUpdate;
        }
    }
}
