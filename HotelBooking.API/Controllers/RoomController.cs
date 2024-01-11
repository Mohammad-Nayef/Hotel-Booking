using AutoMapper;
using FluentValidation;
using HotelBooking.Api.Extensions;
using HotelBooking.Api.Models;
using HotelBooking.Domain.Abstractions.Services;
using HotelBooking.Domain.Constants;
using HotelBooking.Domain.Exceptions;
using HotelBooking.Domain.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using SixLabors.ImageSharp;

namespace HotelBooking.Api.Controllers
{
    [Authorize(Roles = $"{UserRoles.Admin}")]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
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
        /// Add images for a room.
        /// </summary>
        /// <param name="roomId">Id of the room to add images for.</param>        
        /// <response code="404">The room with the given Id doesn't exist.</response>
        /// <response code="204">The images are successfully added.</response>
        [HttpPost("{roomId}/images")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public async Task<IActionResult> PostRoomImagesAsync(Guid roomId, List<IFormFile> imagesForms)
        {
            try
            {
                var images = imagesForms.ToImages();
                await _roomService.AddImagesForRoomAsync(roomId, images);
            }
            catch (UnknownImageFormatException)
            {
                return BadRequest("Invalid image format.");
            }
            catch (ValidationException ex)
            {
                return BadRequest(ex.GetErrorsForClient());
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (EntityImagesLimitExceededException ex)
            {
                return BadRequest(
                    $"The limit of the allowed images per entity ({ex.ExceededLimit}) is exceeded");
            }

            return Created();
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
