namespace CleanArchitecture.API.Errors
{
    public class codeErrorException : CodeErrorResponse
    {
        public string? Details { get; set; }
        public codeErrorException(int statusCode, string? errorMessage = null, string? details = null) : base(statusCode, errorMessage)
        {
            Details = details;
        }
    }
}
