using System.Net;

namespace SquareAPI.DTOs.Responses
{
    /// <summary>
    /// Represents a standard response without data.
    /// Useful for indicating status, messages, and errors in API responses.
    /// </summary>
    public abstract class Response
    {
        public string? Message { get; set; } // Custom error message
        public List<string> Errors { get; set; } = []; // List of encountered errors
        public HttpStatusCode StatusCode { get; set; } // HTTP status code for the response
    }

    /// <summary>
    /// Represents a standard response with data.
    ///</summary>
    public class Response<T> : Response
    {
        public T? Data { get; set; } // The data returned in the response, e.g. a list of squares
    }
}
