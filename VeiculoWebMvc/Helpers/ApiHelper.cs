using System.Net.Http.Headers;
using static VeiculoWebMvc.CommonMethods.StringMethods;

namespace VeiculoWebMvc.Helpers
{
    public class ApiHelper
    {
        public static HttpClient ApiClient { get; set; }

        public static void InitiateClient()
        {
            ApiClient = new HttpClient();
            ApiClient.BaseAddress = new Uri(GetInfoFromAppSettings("BaseApiUrl"));
            ApiClient.DefaultRequestHeaders.Accept.Clear();
            ApiClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

        }
    }
}
