using System.Net;

namespace VeiculoWebMvc.Models
{
    public class ErrorViewModel
    {
        public string? RequestId { get; set; }
        public HttpStatusCode? StatusCode { get; set; }
        public string? ErrorMessage { get; set; }

        public bool ShowRequestId => !string.IsNullOrEmpty(RequestId);
    }
}