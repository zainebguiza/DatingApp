namespace API.Errors
{
    public class ApiException
    {  
        public ApiException(int statusCode,string Message ,string details) 
        {
            this.StatusCode = statusCode;
            this.Message=Message;
            this.Details = details;  
        }
        
        public int StatusCode { get; set; }
        public string Message { get; set; }
        public string Details { get; set; }
    }
}