namespace API.Errors
{
    public class ApIException : ApiResponse
    {
        public ApIException(int statusCode, string message = null, string details=null) : base(statusCode, message)
        {
            Details = details;
        }

        public string Details {get; set;}
    }
}