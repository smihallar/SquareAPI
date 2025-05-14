using SquareAPI.Models;

namespace SquareAPI.Repositories
{
    public interface ISquareRepository
    {
        /// <summary>
        /// Asynchronously retrieves all squares currently in storage.
        /// </summary>
        /// <returns>A task that represents the asynchronous operation. The task result contains a list of <see cref="Square"/> objects,
        /// or an empty list if no squares exist.</returns>
        Task<List<Square>> GetAllSquaresAsync();

        /// <summary>
        /// Asynchronously adds a new square to the system and saves it to persistent storage.
        /// </summary>
        /// <param name="square">The <see cref="Square"/> object to add.</param>
        /// <returns>A task representing the asynchronous operation.</returns>
        Task AddSquareAsync(Square square);

        /// <summary>
        /// Asynchronously resets squares by clearing the existing squares and creating a new empty list. 
        /// </summary>
        /// <returns>A task representing the asynchronous operation.</returns>
        Task ResetSquaresAsync();

        /// <summary>
        /// Asynchronously saves the provided list of squares to persistent storage.
        /// </summary>
        /// <param name="squares">The list of <see cref="Square"/> objects to save.</param>
        /// <returns>A task representing the asynchronous operation.</returns>
        Task SaveSquaresAsync(List<Square> squares);

        /// <summary>
        /// Asynchronously deletes a square at the specified (x, y) position from the current list of squares.
        /// </summary>
        /// <param name="x">The X-coordinate of the square to delete.</param>
        /// <param name="y">The Y-coordinate of the square to delete.</param>
        /// <returns>A task representing the asynchronous operation.</returns>
        Task DeleteSquareByPositionAsync(int x, int y);
    }
}
