using AutoMapper;
using FluentValidation;
using HotelBooking.Api.Extensions;
using HotelBooking.Api.Models;
using HotelBooking.Domain.Abstractions.Services;
using HotelBooking.Domain.Models;
using Microsoft.AspNetCore.JsonPatch;
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

            var createdRoom = _mapper.Map<RoomCreationResponseDTO>(newRoom);
            createdRoom.Id = newId;

            return Created($"api/rooms/{newId}", createdRoom);
        }

        /// <summary>
        /// Delete a room with a specific Id.
        /// </summary>
        /// <param name="roomId">The Id of the room to delete.</param>
        /// <response code="404">The room with the given Id doesn't exist.</response>
        /// <response code="204">The room is deleted successfully.</response>
        [HttpDelete("{id}")]
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
        [HttpPatch("rooms/{roomId}")]
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
