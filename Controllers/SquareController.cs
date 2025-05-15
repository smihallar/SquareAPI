using Microsoft.AspNetCore.Mvc;
using SquareAPI.DTOs;
using SquareAPI.Services;

namespace SquareAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SquareController : ControllerBase
    {
        private readonly ISquareService squareService;

        public SquareController(ISquareService squareService)
        {
            this.squareService = squareService;
        }

        /// <summary>
        /// Retrieves all squares that have been added so far.
        /// </summary>
        /// <returns>
        /// A list of <see cref="SquareDto"/> representing all added squares. 
        /// If no squares are found, a 404 Not Found response is returned.
        /// If an internal error occurs, a 500 Internal Server Error response is returned.
        /// </returns>
        [HttpGet]
        [ProducesResponseType(typeof(List<SquareDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<List<SquareDto>>> GetAllSquares()
        {
            var response = await squareService.GetAllSquaresAsync();

            if (response.IsSuccess)
            {
                return Ok(response.Data);
            }

            return StatusCode((int)response.StatusCode, response.Message);
        }

        /// <summary>
        /// Deletes the last square added.
        /// </summary>
        /// <returns>
        /// - 204 NoContent if the square was successfully deleted.
        /// - Appropriate error message with status code if the deletion fails (e.g., 404 Not Found, 500 Internal Server Error).
        /// </returns>
        [HttpDelete]
        public async Task<ActionResult> DeleteLastSquare()
        {
            var response = await squareService.DeleteLastSquareAsync();
            if (response.IsSuccess == true)
            {
                return NoContent();
            }

            return StatusCode((int)response.StatusCode, response.Message);
        }

        /// <summary>
        /// Resets the squares by clearing the existing squares and creating a new empty list.
        /// </summary>
        /// <returns>A status code indicating success (200 OK) or an error (500 Internal Server Error).</returns>
        [HttpPut]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> ResetSquares()
        {
            var response = await squareService.ResetSquaresAsync();

            if (response.IsSuccess == true)
            {
                return StatusCode(StatusCodes.Status200OK, response.Message);
            }

            return StatusCode((int)response.StatusCode, response.Message);
        }

        /// <summary>
        /// Creates a new square and returns its details.
        /// </summary>
        /// <returns>
        /// A <see cref="SquareCreateDTO"/> if successful, or an error message with status code.
        /// </returns>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<SquareCreateDTO>> AddSquare()
        {
            var response = await squareService.AddSquareAsync();

            if (response.IsSuccess == true)
            {
                return Ok(response.Data);
            }

            return StatusCode((int)response.StatusCode, response.Message);
        }
    }
}
