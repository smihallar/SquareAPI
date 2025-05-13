namespace SquareAPI.DTOs
{
    /// <summary>
    /// A Data Transfer Object (DTO) representing a square in a grid.
    /// </summary>
    public class SquareDTO
    {
        public int X { get; set; } // Horizontal position of the square in the grid
        public int Y { get; set; } // Vertical position of the square in the grid
        public string Color { get; set; } // Color of the square
    }
}
