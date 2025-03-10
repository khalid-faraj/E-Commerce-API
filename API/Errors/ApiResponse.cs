namespace API.Errors
{
	public class ApiResponse
	{
		public string Message { get; set; }
		public int StatusCode { get; set; }
        public ApiResponse(int statusCode, string message = null)
        {
            Message = message ?? GetDefaultMessageForStatusCode(statusCode);
            StatusCode = statusCode;
        }

        public string GetDefaultMessageForStatusCode(int statusCode)
        {
            return statusCode switch
            {
                400 => "There Is A Bad Response!",
                401 => "You Are Not Authorized!",
                404 => "Item Not Found",
                500 => "Sorry, Server Error.",
                _ => null
            };
        }

	}
}
