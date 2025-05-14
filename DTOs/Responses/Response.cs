using System.Net;

namespace SquareAPI.DTOs.Responses
{
    /// <summary>
    /// Represents a standard response without data.
    /// Useful for indicating status, messages, and errors in API responses.
    /// </summary>
    public class Response
    {
        public string? Message { get; set; } // Custom error message
        public List<string> Errors { get; set; } = new List<string>();// List of encountered errors
        public HttpStatusCode StatusCode { get; set; } // HTTP status code for the response
        public bool IsSuccess { get; set; } // Indicates if the operation was successful

        public Response(HttpStatusCode statusCode, string? message = null)
        {
            StatusCode = statusCode;
            Message = message;
            IsSuccess = ((int)statusCode >= 200 && (int)statusCode < 300); // if StatusCode is 200-299 then operation succeeded
        }
    }
    
    /// <summary>
    /// Represents a standard response with data.
    ///</summary>
    public class Response<T> : Response
    {
        public Response(HttpStatusCode statusCode, string? message = null) : base(statusCode, message)
        {
        }

        public T? Data { get; set; } // The data returned in the response, e.g. a list of squares
    }
}
