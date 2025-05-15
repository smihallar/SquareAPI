using SquareAPI.DTOs;
using SquareAPI.DTOs.Responses;
using SquareAPI.Models;
using SquareAPI.Repositories;
using System.Net;

namespace SquareAPI.Services
{
    public class SquareService : ISquareService
    {
        private readonly ISquareRepository repository;
        private readonly Random random;

        public SquareService(ISquareRepository repository, Random random)
        {
            this.repository = repository;
            this.random = random;
        }

        /// <inheritdoc/>
        public async Task<Response<List<SquareDto>>> GetAllSquaresAsync()
        {
            try
            {
                var squares = await repository.GetAllSquaresAsync();
                if (squares == null || squares.Count == 0)
                {
                    // Return a NotFound with no data if no squares are found
                    return new Response<List<SquareDto>>(HttpStatusCode.NotFound, "No squares found")
                    {
                        Data = null
                    };
                }

                // Convert the list of squares to a list of SquareDto
                var squareDtos = squares.Select(s => new SquareDto
                {
                    Index = s.Index,
                    X = s.X,
                    Y = s.Y,
                    Color = s.Color
                }).ToList();

                var sortedSquares = squareDtos.OrderBy(s => s.Index).ToList();

                // Return a successful response with the list of SquareDto
                return new Response<List<SquareDto>>(HttpStatusCode.OK, "Squares fetched successfully")
                {
                    Data = squareDtos
                };
            }
            catch (Exception ex)
            {
                // Return an error response if something goes wrong
                return new Response<List<SquareDto>>(HttpStatusCode.InternalServerError, "Error fetching squares")
                {
                    Errors = new List<string> { ex.Message },
                    Data = null
                };
            }
        }

        /// <inheritdoc/>
        public async Task<Response<SquareCreateDTO>> AddSquareAsync()
        {
            try
            {
                var squares = await repository.GetAllSquaresAsync();

                var squareCount = squares.Count;
                var lastColorUsed = squares.Count > 0 ? squares.Last().Color ?? string.Empty : string.Empty;

                var newSquare = GenerateNextSquare(squareCount, lastColorUsed);
                await repository.AddSquareAsync(newSquare);

                var dto = new SquareCreateDTO
                {
                    X = newSquare.X,
                    Y = newSquare.Y,
                    Color = newSquare.Color
                };

                return new Response<SquareCreateDTO>(HttpStatusCode.Created, "Square added successfully")
                {
                    Data = dto,
                };
            }
            catch (Exception ex)
            {
                return new Response<SquareCreateDTO>(HttpStatusCode.InternalServerError, "Error adding square")
                {
                    Errors = new List<string> { ex.Message },
                    Data = null
                };
            }
        }

        ///<inheritdoc/>
        public async Task<Response> ResetSquaresAsync()
        {
            try
            {
                await repository.ResetSquaresAsync();

                // Return a success response if the reset is successful
                return new Response(HttpStatusCode.OK, "All squares have been successfully reset.");
            }
            catch (Exception ex)
            {
                // Return an error response if something goes wrong
                return new Response(HttpStatusCode.InternalServerError, "Error resetting squares.")
                {
                    Errors = new List<string> { ex.Message }  // Include the exception message in the errors.
                };
            }
        }

        ///<inheritdoc/>
        public async Task<Response> DeleteLastSquareAsync()
        {
            try
            {
                var squares = await repository.GetAllSquaresAsync();

                if (squares.Count == 0)
                {
                    return new Response(HttpStatusCode.NotFound, "No squares to remove");
                }

                var lastIndex = squares.Count - 1;
                var (x, y) = GetPosition(lastIndex);

                await repository.DeleteSquareByPositionAsync(x, y);

                return new Response(HttpStatusCode.NoContent);
            }
            catch (Exception ex)
            {
                return new Response(HttpStatusCode.InternalServerError, "An error occurred while removing the square")
                {
                    Errors = new List<string> { ex.Message }
                };
            }
        }

        private Square GenerateNextSquare(int squareCount, string lastColorUsed)
        {
            var index = squareCount; // The index of the new square = current count of squares since index starts at 0
            var pos = GetPosition(index);
            string newColor = GenerateSquareColor(lastColorUsed);

            return new Square { Index = index, X = pos.x, Y = pos.y, Color = newColor };
        }

        /// <summary>
        /// Calculates the (x, y) position of a square in a grid based on its index (the order in which it was added, 
        /// for example the third square added will have index = 4). 
        /// </summary>
        /// <param name="index">The index of the square.</param>
        /// <returns>
        /// The X and Y coordinates of the square.
        /// The first square (index 0) starts at (0, 0), and subsequent squares
        /// grow outward in layers, moving right, down, and then left to form a stepped pattern.
        /// </returns>
        private (int x, int y) GetPosition(int index)
        {
            if (index == 0) return (0, 0); // If it's the first square added, return (0, 0)
            int layer = 1, count = 1; // Start with the first layer and one square already added
            while (true)
            {
                int baseX = layer, baseY = 0; // The base position for the current layer, i.e. the first square in the layer
                int downSteps = layer, leftSteps = layer; // The number of steps to move down and left in the current layer
                int totalSteps = 1 + downSteps + leftSteps; // Total steps in the current layer, also represents the amount of squares in the layer

                if (index < count + totalSteps) // If the index belongs to current layer
                {
                    int offset = index - count; // Calculate the offset from the start of the layer
                    if (offset == 0) return (baseX, baseY); // The first square in the layer
                    if (offset <= downSteps) return (baseX, baseY + offset); // The squares belonging to the same x coordinate as the base
                    return (baseX - (offset - downSteps), baseY + downSteps); // The squares that "move left" in the layer
                }
                count += totalSteps; // Update the count of squares added so far with amount of squares in the current layer
                layer++; // Move to the next layer
            }
        }

        private string GenerateSquareColor(string lastColorUsed)
        {
            string newColor;
            do
            {
                newColor = String.Format("#{0:X6}", random.Next(0x1000000)); // Generate a random color
            } while (lastColorUsed == newColor); // Ensure it's different from the last square's color

            return newColor;
        }
    }
}
