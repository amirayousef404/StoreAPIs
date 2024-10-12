

namespace Store.G04.APIs.Errors
{
    public class ApiServerErrorResponse : ApiErrorResponse
    {
        public string?  Details { get; set; }

        public ApiServerErrorResponse(int statusCode, string? message = null, string details = null) 
            : base(statusCode, message) 
        {
        
            Details = details;
        }
    }
}
