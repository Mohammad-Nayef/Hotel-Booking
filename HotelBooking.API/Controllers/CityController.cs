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
    [Route("api/cities")]
    public class CityController : Controller
    {
        private readonly ICityService _cityService;
        private readonly IMapper _mapper;

        public CityController(ICityService cityService, IMapper mapper)
        {
            _cityService = cityService;
            _mapper = mapper;
        }

        /// <summary>
        /// Create and store a new city.
        /// </summary>
        /// <param name="newCity">Properties of the new city.</param>
        /// <response code="201">Returns the city with a new Id and its URI in response headers.</response>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(CityCreationDTO), StatusCodes.Status201Created)]
        public async Task<IActionResult> PostCityAsync(CityCreationDTO newCity)
        {
            Guid newId;

            try
            {
                newId = await _cityService.AddAsync(_mapper.Map<CityDTO>(newCity));
            }
            catch (ValidationException ex)
            {
                return BadRequest(ex.GetErrorsForClient());
            }

            var createdCity = _mapper.Map<CityCreationResponseDTO>(newCity);
            createdCity.Id = newId;

            return Created($"api/cities/{newId}", createdCity);
        }

        /// <summary>
        /// Add images for a city.
        /// </summary>
        /// <param name="cityId">Id of the city to add images for.</param>        
        /// /// <response code="404">The city with the given Id doesn't exist.</response>
        /// <response code="204">The images are successfully.</response>
        [HttpPost("{cityId}/images")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public async Task<IActionResult> PostCityImages(Guid cityId, List<IFormFile> imagesForms)
        {
            try
            {
                var images = imagesForms.ToImages();
                await _cityService.AddImagesForCityAsync(cityId, images);
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
        /// Delete a city with a specific Id.
        /// </summary>
        /// <param name="cityId">The Id of the city to delete.</param>
        /// <response code="404">The city with the given Id doesn't exist.</response>
        /// <response code="204">The city is deleted successfully.</response>
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> DeleteAsync(Guid cityId)
        {
            try
            {
                await _cityService.DeleteAsync(cityId);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }

            return NoContent();
        }

        /// <summary>
        /// Partially update a specific city.
        /// </summary>
        /// <param name="cityId">The Id of the city to update.</param>
        /// <param name="cityPatchDocument">Patch operations for (Name, CountryName, PostOffice).</param>
        /// <response code="404">The city with the given Id doesn't exist.</response>
        /// <response code="204">The city is updated successfully.</response>
        [HttpPatch("cities/{cityId}")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> PatchCityAsync(
            Guid cityId, JsonPatchDocument<CityUpdateDTO> cityPatchDocument)
        {
            CityDTO cityToUpdate;

            try
            {
                cityToUpdate = await _cityService.GetByIdAsync(cityId);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }

            var cityToPartiallyUpdate = GetCityToPartiallyUpdate(
                cityPatchDocument, cityToUpdate);

            if (!ModelState.IsValid || !TryValidateModel(cityToPartiallyUpdate))
                return BadRequest(ModelState);

            _mapper.Map(cityToPartiallyUpdate, cityToUpdate);

            try
            {
                await _cityService.UpdateAsync(cityToUpdate);
            }
            catch (ValidationException ex)
            {
                return BadRequest(ex.GetErrorsForClient());
            }

            return NoContent();
        }

        private CityUpdateDTO GetCityToPartiallyUpdate(
            JsonPatchDocument<CityUpdateDTO> cityPatchDocument, CityDTO cityToUpdate)
        {
            var cityToPartiallyUpdate = _mapper.Map<CityUpdateDTO>(cityToUpdate);
            cityPatchDocument.ApplyTo(cityToPartiallyUpdate, ModelState);

            return cityToPartiallyUpdate;
        }
    }
}
