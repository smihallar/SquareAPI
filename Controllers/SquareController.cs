using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SquareAPI.DTOs;
using SquareAPI.Models;
using SquareAPI.Services;
using System.Net;

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

            switch (response.StatusCode)
            {
                case HttpStatusCode.NotFound:
                    return NotFound(response);
                case HttpStatusCode.InternalServerError:
                    return StatusCode(StatusCodes.Status500InternalServerError, response);
                default:
                    return Ok(response.Data);
            }
        }

        [HttpDelete]
        public async Task<ActionResult> DeleteLastSquare()
        {
            return Ok();
        }

        /// <summary>
        /// Adds a new square.
        /// </summary>
        /// <returns>StatusCode OK or error message with appropriate StatusCode</returns>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> AddSquare()
        {
            var response = await squareService.AddSquareAsync();

            if (response.StatusCode == HttpStatusCode.Created)
            {
                return Ok();
            }
            else
            {
                return StatusCode((int)response.StatusCode, response.Message);
            }
        }


    }
}
