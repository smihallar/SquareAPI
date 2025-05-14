namespace SquareAPI.Models
{
    /// <summary>
    /// A model to represent a square in a grid.
    /// </summary>
    public class Square
    {
        /// <summary>
        /// Horizontal grid coordinate of the square.
        /// </summary>
        public int X { get; set; }
        /// <summary>
        /// Vertical grid coordinate of the square.
        /// </summary>
        public int Y { get; set; }
        /// <summary>
        /// Color of the square in hexadecimal format (e.g., #FF5733).
        /// </summary>
        public string Color { get; set; }
    }
}
