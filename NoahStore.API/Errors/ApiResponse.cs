namespace NoahStore.API.Errors
{
    public class ApiResponse
    {
        public ApiResponse(int statusCode, string? message)
        {
            StatusCode = statusCode;
            Message = message?? GetStatusMessage(statusCode);
        }

        public int StatusCode { get; set; }
        public string? Message { get; set; }

        public static string GetStatusMessage(int statusCode)
        {
            return statusCode switch
            {
                200 => "The request was successful.",
                400 => "Bad request",
                401 => "Authentication failed.",
                403 => "The client does not have permission to access the requested resource.",
                404 => "Resource not found.",
                405 => "Method not allowed.",
                409 => "Conflict.",
                422 => "Validation failed.",
                500 => "An unexpected error occurred on the server.",
                501 => "The server does not support the functionality required to fulfill the request.",
                503 => "The server is temporarily unable to handle the request.",
                _ => "Unknown Status"
            };
        }
    }
}
