using SquareAPI.Models;

namespace SquareAPI.Repositories
{
    public interface ISquareRepository
    {
        Task<List<Square>> GetAllSquaresAsync();
        Task AddSquareAsync(Square square);
        Task ResetSquaresAsync();
    }
}
