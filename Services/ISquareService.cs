using SquareAPI.DTOs;
using SquareAPI.DTOs.Responses;

namespace SquareAPI.Services
{
    /// <summary>
    /// Interface for the Square service which defines methods for managing squares.
    /// </summary>
    public interface ISquareService
    {
        /// <summary>
        /// Asynchronously retrieves all squares that have been added.
        /// </summary>
        /// <returns>
        /// A <see cref="Response{T}"/> containing:
        /// - A list of <see cref="SquareDto"/>
        /// - A status code: 200 (OK), or 500 (Internal Server Error)
        /// - A success flag
        /// - Any errors and a custom message
        /// </returns>
        Task<Response<List<SquareDto>>> GetAllSquaresAsync();

        /// <summary>
        /// Asynchronously adds a new square to the system.
        /// </summary>
        /// <returns>
        /// A <see cref="Response{SquareCreateDTO}"/> object containing the result:
        /// - HTTP 201 Created with square data on success
        /// - HTTP 500 Internal Server Error with error details on failure
        /// </returns>
        Task<Response<SquareCreateDTO>> AddSquareAsync();

        /// <summary>
        /// Asynchronously resets the list of squares by clearing existing entries.
        /// </summary>
        /// <returns>
        /// A <see cref="Response"/> containing:
        /// - A status code: 200 (OK) or 500 (Internal Server Error)
        /// - A success flag
        /// - Any errors and a custom message
        /// </returns>
        Task<Response> ResetSquaresAsync();

        /// <summary>
        /// Asynchronously deletes the last square from the list.
        /// </summary>
        /// <returns>
        /// A <see cref="Response"/> containing:
        /// - A status code: 200 (OK), 404 (Not Found), or 500 (Internal Server Error)
        /// - A success flag
        /// - Any errors and a custom message
        /// </returns>
        Task<Response> DeleteLastSquareAsync();
    }
}
