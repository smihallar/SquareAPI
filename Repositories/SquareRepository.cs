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

            try
            {
                var json = await File.ReadAllTextAsync(FilePath);
                return JsonSerializer.Deserialize<List<Square>>(json) ?? new List<Square>();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error reading or deserializing file: {ex.Message}");
                return new List<Square>();
            }
        }

        /// <inheritdoc/>
        public async Task AddSquareAsync(Square square)
        {
            try
            {
                var squares = await GetAllSquaresAsync();
                squares.Add(square);
                await SaveSquaresAsync(squares);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error adding square: {ex.Message}");
            }
        }

        /// <inheritdoc/>
        public async Task ResetSquaresAsync()
        {
            try
            {
                await SaveSquaresAsync(new List<Square>());
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error resetting squares: {ex.Message}");
            }
        }

        /// <inheritdoc/>
        public async Task SaveSquaresAsync(List<Square> squares)
        {
            try
            {
                var json = JsonSerializer.Serialize(squares, new JsonSerializerOptions { WriteIndented = true });
                await File.WriteAllTextAsync(FilePath, json);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error writing to file: {ex.Message}");
            }
        }

        /// <inheritdoc/>
        public async Task DeleteSquareByPositionAsync(int x, int y)
        {
            try
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
            catch (Exception ex)
            {
                Console.WriteLine($"Error deleting square at ({x}, {y}): {ex.Message}");
            }
        }
    }
}
