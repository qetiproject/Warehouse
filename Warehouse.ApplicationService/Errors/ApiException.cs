namespace Warehouse.ApplicationService.Errors
{
    public class ApiException
    {
        public ApiException(
            int _statusCode, 
            string _message = null, 
            string _details = null
        )
        {
            StatusCode = _statusCode;
            Message = _message;
            Details = _details;
        }
        public int StatusCode { get; set; }
        public string Message { get; set; }
        public string Details { get; set; }
    }
}
