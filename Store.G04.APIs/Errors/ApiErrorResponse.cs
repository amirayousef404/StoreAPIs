namespace Store.G04.APIs.Errors
{
    public class ApiErrorResponse
    {
        public string? Message { get; set; }

        public int StatusCode { get; set; }

        public ApiErrorResponse(int statusCode, string? message = null) 
        {
            StatusCode = statusCode;
            Message = message ?? GetDefaultMessageForStatusCode(statusCode);
        }

        private string? GetDefaultMessageForStatusCode(int statusCode)
        {
            var message = statusCode switch
            {
                400 => "bad request, you have made",
                401 => "Authorized u r not",
                404 => "Resource is not found !",
                500 => "Server Error",
                _ => null
            };

            return message; 
        }
    }
}
