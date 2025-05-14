namespace SquareAPI.DTOs
{
    /// <summary>
    /// Represents the data returned for a square.
    /// </summary>
    public class SquareDto
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
