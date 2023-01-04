using System.Net;

namespace Domain
{
    public class BaseResponseModel
    {
        public string SuccessMessage { get; set; }
        public string ErrorMessage { get; set; }
        public HttpStatusCode StatusCode { get; set; }
    }
}
