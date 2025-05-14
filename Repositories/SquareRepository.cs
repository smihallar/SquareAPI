using SquareAPI.Models;
using System.Text.Json;

namespace SquareAPI.Repositories
{
    /// <inheritdoc/>
    public class SquareRepository : ISquareRepository
    {
        private const string FilePath = "squares.json";

        /// <inheritdoc/>
        public async Task<List<Square>> GetAllSquaresAsync()
        {
            if (!File.Exists(FilePath))
                return new List<Square>();

            var json = await File.ReadAllTextAsync(FilePath);
            return JsonSerializer.Deserialize<List<Square>>(json) ?? new List<Square>();
        }

        /// <inheritdoc/>
        public async Task AddSquareAsync(Square square)
        {
            var squares = await GetAllSquaresAsync();
            squares.Add(square);
            await SaveSquaresAsync(squares);
        }

        /// <inheritdoc/>
        public async Task ResetSquaresAsync()
        {
            await SaveSquaresAsync(new List<Square>());
        }

        /// <inheritdoc/>
        public async Task SaveSquaresAsync(List<Square> squares)
        {
            var json = JsonSerializer.Serialize(squares, new JsonSerializerOptions { WriteIndented = true });
            await File.WriteAllTextAsync(FilePath, json);
        }

        /// <inheritdoc/>
        public async Task DeleteSquareByPositionAsync(int x, int y)
        {
            var squares = await GetAllSquaresAsync();

            var squareToRemove = squares.FirstOrDefault(s => s.X == x && s.Y == y);

            if (squareToRemove != null)
            {
                squares.Remove(squareToRemove);
                await SaveSquaresAsync(squares);
            }
            else
            {
                Console.WriteLine($"Square with position ({x}, {y}) not found.");
            }
        }
    }
}
