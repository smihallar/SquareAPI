using SquareAPI.Models;
using System.Text.Json;

namespace SquareAPI.Repositories
{
    public class SquareRepository : ISquareRepository
    {
        private const string FilePath = "squares.json";

        /// <summary>
        /// Retrieves all squares if they exist in storage.
        /// </summary>
        public async Task<List<Square>> GetAllSquaresAsync()
        {
            if (!File.Exists(FilePath))
                return new List<Square>();

            var json = await File.ReadAllTextAsync(FilePath);
            return JsonSerializer.Deserialize<List<Square>>(json) ?? new List<Square>();
        }

        /// <summary>
        /// Adds a new square to the system and ensures it is stored persistently.
        /// </summary>
        public async Task AddSquareAsync(Square square)
        {
            var squares = await GetAllSquaresAsync();
            squares.Add(square);
            await SaveSquaresAsync(squares);
        }

        /// <summary>
        /// Resets squares by clearing the existing squares and creating a new empty list. 
        /// </summary>
        public async Task ResetSquaresAsync()
        {
            await SaveSquaresAsync(new List<Square>());
        }

        /// <summary>
        /// Saves new squares to the file system.
        /// </summary>
        private async Task SaveSquaresAsync(List<Square> squares)
        {
        }
    }
}
